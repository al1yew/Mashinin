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
        private async Task<List<ModelGetDTO>> RetrieveModels()
        {
            return await _unitOfWork.ModelRepository.GetFilteredAsync(
                x => new ModelGetDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    TurboAzId = x.TurboAzId,
                    CreatedAt = x.CreatedAt.Value.ToString("dd.MM.yyyy HH:mm:ss"),
                    DeletedAt = x.DeletedAt.Value.ToString("dd.MM.yyyy HH:mm:ss"),
                    IsDeleted = x.IsDeleted,
                    IsUpdated = x.IsUpdated,
                    UpdatedAt = x.UpdatedAt.Value.ToString("dd.MM.yyyy HH:mm:ss"),
                    Class = x.Class,
                    Make = x.Make.Name,
                    MakeId = x.MakeId,
                    MakeTurboAzId = x.Make.TurboAzId
                },
                includes: "Make"
            );
        }

        private async Task UpdateCache()
        {
            List<ModelGetDTO> models = await RetrieveModels();

            await SetCache(models);
        }

        private async Task SetCache(List<ModelGetDTO> models)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromDays(1))
                    .SetPriority(CacheItemPriority.Low);

            _memoryCache.Set(cacheKey, models, cacheEntryOptions);
        }

        public async Task<List<ModelGetDTO>> GetAsync()
        {
            List<ModelGetDTO> models;

            if (!_memoryCache.TryGetValue(cacheKey, out models))
            {
                models = await RetrieveModels();

                await SetCache(models);
            }

            return models;
        }

        public async Task<ModelGetDTO> GetAsync(int id)
        {
            List<ModelGetDTO> models = await GetAsync();
            ModelGetDTO model = models.FirstOrDefault(x => x.Id == id);

            if (model is null)
                throw new NotFoundException(_sharedLocalizer["modelNotFound"]);

            return model;
        }


        public async Task<List<ModelGetDTO>> GetByMakeIdAsync(int id)
        {
            List<ModelGetDTO> models = await GetAsync();
            models = models.Where(x => x.MakeId == id).ToList();

            return models;
        }

        public async Task<ModelGetDTO> GetByTurboAzIdAsync(int id)
        {
            List<ModelGetDTO> models = await GetAsync();
            ModelGetDTO model = models.FirstOrDefault(x => x.TurboAzId == id);

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
            x.Name.ToLower() == modelCreateDTO.Name.Trim().ToLower() ||
            x.TurboAzId == modelCreateDTO.TurboAzId);

            if (modelExists)
                throw new RecordDuplicateException(
                    string.Format(_sharedLocalizer["modelExists"],
                    modelCreateDTO.Name,
                    modelCreateDTO.TurboAzId,
                    modelCreateDTO.MakeId)
                    );

            Model model = _mapper.Map<Model>(modelCreateDTO);

            await _unitOfWork.ModelRepository.AddAsync(model);
            await _unitOfWork.CommitAsync();
            await UpdateCache();
        }

        public async Task UpdateAsync(int id, ModelUpdateDTO modelUpdateDTO)
        {
            if (id != modelUpdateDTO.Id)
                throw new BadRequestException(_sharedLocalizer["idsAreDifferent"]);

            if (modelUpdateDTO is null)
                throw new BadRequestException(_sharedLocalizer["objectIsNull"]);

            bool makeExists = await _unitOfWork.MakeRepository.DoesExistAsync(x => x.Id == modelUpdateDTO.MakeId);

            if (!makeExists)
                throw new NotFoundException(_sharedLocalizer["makeNotFound"]);

            bool modelExists = await _unitOfWork.ModelRepository.DoesExistAsync(x =>
            x.Id != modelUpdateDTO.Id &&
            (x.Name.ToLower() == modelUpdateDTO.Name.Trim().ToLower() ||
            x.TurboAzId == modelUpdateDTO.TurboAzId));

            if (modelExists)
                throw new RecordDuplicateException(
                    string.Format(_sharedLocalizer["modelExists"],
                    modelUpdateDTO.Name,
                    modelUpdateDTO.TurboAzId,
                    modelUpdateDTO.MakeId)
                    );

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

        public async Task PermanentDelete(int id)
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
