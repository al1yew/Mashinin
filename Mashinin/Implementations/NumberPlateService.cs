using AutoMapper;
using Mashinin.DTOs.CityDTOs;
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


        private async Task<List<NumberPlateGetDTO>> RetrieveNumberPlates()
        {
            return await _unitOfWork.NumberPlateRepository.GetFilteredAsync(
                x => new NumberPlateGetDTO
                {
                    Id = x.Id,
                    Description = x.Description,
                    IsForBargain = x.IsForBargain,
                    Value = x.Value,
                    ViewCount = x.ViewCount,
                    CreatedAt = x.CreatedAt.Value.ToString("dd.MM.yyyy HH:mm:ss"),
                    DeletedAt = x.DeletedAt.Value.ToString("dd.MM.yyyy HH:mm:ss"),
                    IsDeleted = x.IsDeleted,
                    IsUpdated = x.IsUpdated,
                    UpdatedAt = x.UpdatedAt.Value.ToString("dd.MM.yyyy HH:mm:ss")
                }
            );
        }

        private async Task UpdateCache()
        {
            List<NumberPlateGetDTO> numberPlates = await RetrieveNumberPlates();

            await SetCache(numberPlates);
        }

        private async Task SetCache(List<NumberPlateGetDTO> numberPlates)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromDays(1))
                    .SetPriority(CacheItemPriority.Low);

            _memoryCache.Set(cacheKey, numberPlates, cacheEntryOptions);
        }

        public async Task<List<NumberPlateGetDTO>> GetAsync()
        {
            List<NumberPlateGetDTO> numberPlates;

            if (!_memoryCache.TryGetValue(cacheKey, out numberPlates))
            {
                numberPlates = await RetrieveNumberPlates();

                await SetCache(numberPlates);
            }

            return numberPlates;
        }

        public async Task<NumberPlateGetDTO> GetAsync(int id)
        {
            List<NumberPlateGetDTO> numberPlates = await GetAsync();
            NumberPlateGetDTO numberPlate = numberPlates.FirstOrDefault(x => x.Id == id);

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
                throw new RecordDuplicateException(
                    string.Format(_sharedLocalizer["numberPlateExists"],
                    numberPlateCreateDTO.Value)
                    );

            NumberPlate numberPlate = _mapper.Map<NumberPlate>(numberPlateCreateDTO);

            await _unitOfWork.NumberPlateRepository.AddAsync(numberPlate);
            await _unitOfWork.CommitAsync();
            await UpdateCache();
        }

        public async Task UpdateAsync(int id, NumberPlateUpdateDTO numberPlateUpdateDTO)
        {
            if (id != numberPlateUpdateDTO.Id)
                throw new BadRequestException(_sharedLocalizer["idsAreDifferent"]);

            if (numberPlateUpdateDTO is null)
                throw new BadRequestException(_sharedLocalizer["objectIsNull"]);

            bool numberPlateExists = await _unitOfWork.NumberPlateRepository.DoesExistAsync(x =>
            x.Id != numberPlateUpdateDTO.Id &&
            x.Value.ToLower() == numberPlateUpdateDTO.Value.Trim().ToLower());

            if (numberPlateExists)
                throw new RecordDuplicateException(
                    string.Format(_sharedLocalizer["numberPlateExists"],
                    numberPlateUpdateDTO.Value)
                    );

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
