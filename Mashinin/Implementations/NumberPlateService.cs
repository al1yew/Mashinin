using AutoMapper;
using Mashinin.DTOs.NumberPlateDTOs;
using Mashinin.Entities;
using Mashinin.Exceptions;
using Mashinin.Interfaces;
using Mashinin.Localization;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;

namespace Mashinin.Implementations
{
    public class NumberPlateService : INumberPlateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly string cacheKey = "NumberPlates";
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public NumberPlateService(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache memoryCache, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _memoryCache = memoryCache;
            _sharedLocalizer = sharedLocalizer;
        }

        private async Task UpdateCache()
        {
            List<NumberPlateGetDTO> numberPlates = _mapper.Map<List<NumberPlateGetDTO>>(await _unitOfWork.NumberPlateRepository.GetAllAsync());

            await SetCache(numberPlates);
        }
        private async Task SetCache(List<NumberPlateGetDTO> numberPlates)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                    .SetPriority(CacheItemPriority.Low);

            _memoryCache.Set(cacheKey, numberPlates, cacheEntryOptions);
        }

        public async Task<List<NumberPlateGetDTO>> GetAsync()
        {
            List<NumberPlateGetDTO> numberPlates;

            if (!_memoryCache.TryGetValue(cacheKey, out numberPlates))
            {
                numberPlates = _mapper.Map<List<NumberPlateGetDTO>>(await _unitOfWork.NumberPlateRepository.GetAllAsync());

                await SetCache(numberPlates);
            }

            return numberPlates;
        }

        public async Task<NumberPlateGetDTO> GetAsync(int id)
        {
            NumberPlateGetDTO numberPlate = _mapper.Map<NumberPlateGetDTO>(await _unitOfWork.NumberPlateRepository.GetAsync(x => x.Id == id));

            if (numberPlate is null)
                throw new NotFoundException(_sharedLocalizer["numberPlateNotFound"]);

            return numberPlate;
        }

        public async Task CreateAsync(NumberPlateCreateDTO numberPlateCreateDTO)
        {
            if (numberPlateCreateDTO is null)
                throw new BadRequestException(_sharedLocalizer["objectIsNull"]);

            bool numberPlateExists = await _unitOfWork.NumberPlateRepository.DoesExistAsync(x =>
            x.Value.ToLower() == numberPlateCreateDTO.Value.Trim().ToLower());

            if (numberPlateExists)
                throw new RecordDuplicateException(string.Format(_sharedLocalizer["numberPlateExists"], numberPlateCreateDTO.Value));

            NumberPlate numberPlate = _mapper.Map<NumberPlate>(numberPlateCreateDTO);

            await _unitOfWork.NumberPlateRepository.AddAsync(numberPlate);
            await _unitOfWork.CommitAsync();
            await UpdateCache();
        }

        public async Task UpdateAsync(NumberPlateUpdateDTO numberPlateUpdateDTO)
        {
            if (numberPlateUpdateDTO is null)
                throw new BadRequestException(_sharedLocalizer["objectIsNull"]);

            //id is not the same, but values are the same
            bool numberPlateExists = await _unitOfWork.NumberPlateRepository.DoesExistAsync(x =>
            x.Id != numberPlateUpdateDTO.Id &&
            x.Value.ToLower() == numberPlateUpdateDTO.Value.Trim().ToLower());

            if (numberPlateExists)
                throw new RecordDuplicateException(string.Format(_sharedLocalizer["numberPlateExists"], numberPlateUpdateDTO.Value));

            NumberPlate numberPlate = await _unitOfWork.NumberPlateRepository.GetAsync(x => x.Id == numberPlateUpdateDTO.Id);

            if (numberPlate is null)
                throw new NotFoundException(_sharedLocalizer["numberPlateNotFound"]);

            numberPlate.Value = numberPlateUpdateDTO.Value.Trim();
            numberPlate.Description = numberPlateUpdateDTO.Description.Trim();
            numberPlate.IsForBargain = numberPlateUpdateDTO.IsForBargain;
            numberPlate.UpdatedAt = DateTime.UtcNow.AddHours(4);
            numberPlate.IsUpdated = true;

            await _unitOfWork.CommitAsync();
            await UpdateCache();
        }

        public async Task DeleteAsync(int id)
        {
            NumberPlate numberPlate = await _unitOfWork.NumberPlateRepository.GetAsync(x => x.Id == id);

            if (numberPlate is null)
                throw new NotFoundException(_sharedLocalizer["numberPlateNotFound"]);

            numberPlate.IsDeleted = true;
            numberPlate.DeletedAt = DateTime.UtcNow.AddHours(4);

            await _unitOfWork.CommitAsync();
            await UpdateCache();
        }

        public async Task RestoreAsync(int id)
        {
            NumberPlate numberPlate = await _unitOfWork.NumberPlateRepository.GetAsync(x => x.Id == id);

            if (numberPlate is null)
                throw new NotFoundException(_sharedLocalizer["numberPlateNotFound"]);

            numberPlate.IsDeleted = false;
            numberPlate.DeletedAt = null;

            await _unitOfWork.CommitAsync();
            await UpdateCache();
        }
    }
}
