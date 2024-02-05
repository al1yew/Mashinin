using AutoMapper;
using Mashinin.DTOs.CityDTOs;
using Mashinin.Entities;
using Mashinin.Exceptions;
using Mashinin.Interfaces;
using Mashinin.Localization;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;

namespace Mashinin.Implementations
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly string cacheKey = "Cities";
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public CityService(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache memoryCache, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _memoryCache = memoryCache;
            _sharedLocalizer = sharedLocalizer;
        }

        public async Task CreateCities()
        {
            //string json = "in txt file";

            //List<City> cities = new List<City>();

            //cities = JsonConvert.DeserializeObject<List<City>>(json);

            //foreach (City city in cities)
            //{
            //    city.CreatedAt = DateTime.UtcNow.AddHours(4);
            //}

            //await _unitOfWork.CityRepository.AddRangeAsync(cities);
            //await _unitOfWork.CommitAsync();
        }
        private async Task UpdateCache()
        {
            List<CityGetDTO> cities = _mapper.Map<List<CityGetDTO>>(await _unitOfWork.CityRepository.GetAllAsync());

            await SetCache(cities);
        }
        private async Task SetCache(List<CityGetDTO> cities)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                    .SetPriority(CacheItemPriority.Low);

            _memoryCache.Set(cacheKey, cities, cacheEntryOptions);
        }

        public async Task<List<CityGetDTO>> GetAsync()
        {
            List<CityGetDTO> cities;

            if (!_memoryCache.TryGetValue(cacheKey, out cities))
            {
                cities = _mapper.Map<List<CityGetDTO>>(await _unitOfWork.CityRepository.GetAllAsync());

                await SetCache(cities);
            }

            return cities;
        }

        public async Task<CityGetDTO> GetAsync(int id)
        {
            CityGetDTO city = _mapper.Map<CityGetDTO>(await _unitOfWork.CityRepository.GetAsync(x => x.Id == id));

            if (city is null)
                throw new NotFoundException(_sharedLocalizer["cityNotFound"]);

            return city;
        }

        public async Task CreateAsync(CityCreateDTO cityCreateDTO)
        {
            if (cityCreateDTO is null)
                throw new BadRequestException(_sharedLocalizer["objectIsNull"]);

            bool cityExists = await _unitOfWork.CityRepository.DoesExistAsync(x =>
            x.NameAz.ToLower() == cityCreateDTO.NameAz.Trim().ToLower() &&
            x.NameRu.ToLower() == cityCreateDTO.NameRu.Trim().ToLower() &&
            x.NameEn.ToLower() == cityCreateDTO.NameEn.Trim().ToLower());

            if (cityExists)
                throw new RecordDuplicateException(string.Format(_sharedLocalizer["cityExists"], cityCreateDTO.NameAz, cityCreateDTO.NameRu, cityCreateDTO.NameEn));

            City city = _mapper.Map<City>(cityCreateDTO);

            await _unitOfWork.CityRepository.AddAsync(city);
            await _unitOfWork.CommitAsync();
            await UpdateCache();
        }

        public async Task UpdateAsync(CityUpdateDTO cityUpdateDTO)
        {
            if (cityUpdateDTO is null)
                throw new BadRequestException(_sharedLocalizer["objectIsNull"]);

            //id is not the same, but values are the same
            bool cityExists = await _unitOfWork.CityRepository.DoesExistAsync(x =>
            x.Id != cityUpdateDTO.Id &&
            x.NameAz.ToLower() == cityUpdateDTO.NameAz.Trim().ToLower() &&
            x.NameRu.ToLower() == cityUpdateDTO.NameRu.Trim().ToLower() &&
            x.NameEn.ToLower() == cityUpdateDTO.NameEn.Trim().ToLower());

            if (cityExists)
                throw new RecordDuplicateException(string.Format(_sharedLocalizer["cityExists"], cityUpdateDTO.NameAz, cityUpdateDTO.NameRu, cityUpdateDTO.NameEn));

            City city = await _unitOfWork.CityRepository.GetAsync(x => x.Id == cityUpdateDTO.Id);

            if (city is null)
                throw new NotFoundException(_sharedLocalizer["cityNotFound"]);

            city.NameAz = cityUpdateDTO.NameAz.Trim();
            city.NameRu = cityUpdateDTO.NameRu.Trim();
            city.NameEn = cityUpdateDTO.NameEn.Trim();
            city.UpdatedAt = DateTime.UtcNow.AddHours(4);
            city.IsUpdated = true;

            await _unitOfWork.CommitAsync();
            await UpdateCache();
        }

        public async Task DeleteAsync(int id)
        {
            City city = await _unitOfWork.CityRepository.GetAsync(x => x.Id == id);

            if (city is null)
                throw new NotFoundException(_sharedLocalizer["cityNotFound"]);

            city.IsDeleted = true;
            city.DeletedAt = DateTime.UtcNow.AddHours(4);

            await _unitOfWork.CommitAsync();
            await UpdateCache();
        }

        public async Task RestoreAsync(int id)
        {
            City city = await _unitOfWork.CityRepository.GetAsync(x => x.Id == id);

            if (city is null)
                throw new NotFoundException(_sharedLocalizer["cityNotFound"]);

            city.IsDeleted = false;
            city.DeletedAt = null;

            await _unitOfWork.CommitAsync();
            await UpdateCache();
        }

        public async Task DeleteForeverAsync(int id)
        {
            City city = await _unitOfWork.CityRepository.GetAsync(x => x.Id == id);

            if (city is null)
                throw new NotFoundException(_sharedLocalizer["cityNotFound"]);

            _unitOfWork.CityRepository.Remove(city);
            await _unitOfWork.CommitAsync();
            await UpdateCache();
        }
    }
}
