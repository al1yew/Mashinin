using AutoMapper;
using Mashinin.DTOs.ModelDTOs;
using Mashinin.Entities;
using Mashinin.Exceptions;
using Mashinin.Interfaces;
using Mashinin.Localization;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;

namespace Mashinin.Implementations
{
    public class ModelService : IModelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly string cacheKey = "Models";
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        public ModelService(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache memoryCache, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _memoryCache = memoryCache;
            _sharedLocalizer = sharedLocalizer;
        }

        public async Task CreateModels()
        {
            //string json = "find in txt file";
            //List<Model> models = new List<Model>();

            //models = JsonConvert.DeserializeObject<List<Model>>(json);

            //foreach (Model model in models)
            //{
            //    model.CreatedAt = DateTime.UtcNow.AddHours(4);

            //    Make make = await _unitOfWork.MakeRepository.GetAsync(x => x.TurboAzId == model.MakeId);

            //    model.MakeId = 0;

            //    model.Make = make;
            //}

            //await _unitOfWork.ModelRepository.AddRangeAsync(models);
            //await _unitOfWork.CommitAsync();
        }
        private async Task UpdateCache()
        {
            List<ModelGetDTO> models = _mapper.Map<List<ModelGetDTO>>(await _unitOfWork.ModelRepository.GetAllAsync());

            await SetCache(models);
        }
        private async Task SetCache(List<ModelGetDTO> models)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                    .SetPriority(CacheItemPriority.Low);

            _memoryCache.Set(cacheKey, models, cacheEntryOptions);
        }

        public async Task<List<ModelGetDTO>> GetAsync()
        {
            List<ModelGetDTO> models;

            if (!_memoryCache.TryGetValue(cacheKey, out models))
            {
                models = _mapper.Map<List<ModelGetDTO>>(await _unitOfWork.ModelRepository.GetAllAsync("Make"));

                await SetCache(models);
            }

            return models;
        }

        public async Task<List<ModelGetDTO>> GetByMakeIdAsync(int id)
        {
            List<ModelGetDTO> models = _mapper.Map<List<ModelGetDTO>>(await _unitOfWork.ModelRepository.GetAllByExAsync(x => x.MakeId == id, "Make"));

            return models;
        }

        public async Task<ModelGetDTO> GetAsync(int id)
        {
            ModelGetDTO model = _mapper.Map<ModelGetDTO>(await _unitOfWork.ModelRepository.GetAsync(x => x.Id == id, "Make"));

            if (model is null)
                throw new NotFoundException(_sharedLocalizer["modelNotFound"]);

            return model;
        }

        public async Task<ModelGetDTO> GetByTurboAzIdAsync(int id)
        {
            ModelGetDTO model = _mapper.Map<ModelGetDTO>(await _unitOfWork.ModelRepository.GetAsync(x => x.TurboAzId == id, "Make"));

            if (model is null)
                throw new NotFoundException(_sharedLocalizer["modelNotFound"]);

            return model;
        }

        public async Task CreateAsync(ModelCreateDTO modelCreateDTO)
        {
            if (modelCreateDTO is null)
                throw new BadRequestException(_sharedLocalizer["objectIsNull"]);

            bool makeExists = await _unitOfWork.MakeRepository.DoesExistAsync(x => x.Id == modelCreateDTO.MakeId);

            if (!makeExists)
                throw new NotFoundException(_sharedLocalizer["makeNotFound"]);

            bool modelExists = await _unitOfWork.ModelRepository.DoesExistAsync(x =>
            x.Name.ToLower() == modelCreateDTO.Name.Trim().ToLower() &&
            x.TurboAzId == modelCreateDTO.TurboAzId &&
            x.MakeId == modelCreateDTO.MakeId);

            if (modelExists)
                throw new RecordDuplicateException(string.Format(_sharedLocalizer["modelExists"], modelCreateDTO.Name, modelCreateDTO.TurboAzId, modelCreateDTO.MakeId));

            Model model = _mapper.Map<Model>(modelCreateDTO);

            await _unitOfWork.ModelRepository.AddAsync(model);
            await _unitOfWork.CommitAsync();
            await UpdateCache();
        }

        public async Task UpdateAsync(ModelUpdateDTO modelUpdateDTO)
        {
            if (modelUpdateDTO is null)
                throw new BadRequestException(_sharedLocalizer["objectIsNull"]);

            bool makeExists = await _unitOfWork.MakeRepository.DoesExistAsync(x => x.Id == modelUpdateDTO.MakeId);

            if (!makeExists)
                throw new NotFoundException(_sharedLocalizer["makeNotFound"]);

            //id is not the same, but values are the same
            bool modelExists = await _unitOfWork.ModelRepository.DoesExistAsync(x =>
            x.Id != modelUpdateDTO.Id &&
            x.Name.ToLower() == modelUpdateDTO.Name.Trim().ToLower() &&
            x.TurboAzId == modelUpdateDTO.TurboAzId &&
            x.MakeId == modelUpdateDTO.MakeId);

            if (modelExists)
                throw new RecordDuplicateException(string.Format(_sharedLocalizer["modelExists"], modelUpdateDTO.Name, modelUpdateDTO.TurboAzId, modelUpdateDTO.MakeId));

            Model model = await _unitOfWork.ModelRepository.GetAsync(x => x.Id == modelUpdateDTO.Id);

            if (model is null)
                throw new NotFoundException(_sharedLocalizer["modelNotFound"]);

            model.Name = modelUpdateDTO.Name.Trim();
            model.TurboAzId = modelUpdateDTO.TurboAzId;
            model.MakeId = modelUpdateDTO.MakeId;
            model.UpdatedAt = DateTime.UtcNow.AddHours(4);
            model.IsUpdated = true;

            await _unitOfWork.CommitAsync();
            await UpdateCache();
        }

        public async Task DeleteAsync(int id)
        {
            Model model = await _unitOfWork.ModelRepository.GetAsync(x => x.Id == id);

            if (model is null)
                throw new NotFoundException(_sharedLocalizer["modelNotFound"]);

            model.IsDeleted = true;
            model.DeletedAt = DateTime.UtcNow.AddHours(4);

            await _unitOfWork.CommitAsync();
            await UpdateCache();
        }

        public async Task RestoreAsync(int id)
        {
            Model model = await _unitOfWork.ModelRepository.GetAsync(x => x.Id == id);

            if (model is null)
                throw new NotFoundException(_sharedLocalizer["modelNotFound"]);

            model.IsDeleted = false;
            model.DeletedAt = null;

            await _unitOfWork.CommitAsync();
            await UpdateCache();
        }

        public async Task DeleteForeverAsync(int id)
        {
            Model model = await _unitOfWork.ModelRepository.GetAsync(x => x.Id == id);

            if (model is null)
                throw new NotFoundException(_sharedLocalizer["modelNotFound"]);

            _unitOfWork.ModelRepository.Remove(model);
            await _unitOfWork.CommitAsync();
            await UpdateCache();
        }
    }
}
