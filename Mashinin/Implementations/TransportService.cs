using AutoMapper;
using Mashinin.DTOs.ColorDTOs;
using Mashinin.DTOs.TransportDTOs;
using Mashinin.Exceptions;
using Mashinin.Interfaces;
using Mashinin.Localization;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;

namespace Mashinin.Implementations
{
    public class TransportService : ITransportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly string cacheKey = "Transports";
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        public TransportService(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache memoryCache, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _memoryCache = memoryCache;
            _sharedLocalizer = sharedLocalizer;
        }

        private async Task UpdateCache()
        {
            //gde mi delaem get? ya ne dumayu shto nam v List Get nujni vse eti inkludi zad.
            //nam cisto nujno budet basic data, motor, god, probeg, fotki
            // no problema v tom shto men listi gerek gonderim fronta, ptm shto filtraciya je est! mi doljni umet delat filtraciyu,
            // poetomu vse taki vse pridetsa otpravit v front, s inkludami vmeste...

            List<TransportGetDTO> transports = _mapper.Map<List<TransportGetDTO>>(
                await _unitOfWork.TransportRepository.GetAllAsync(
                    "Make", "Model", "City", "Color", "Prices", "TransportImages"));

            await SetCache(transports);
        }

        private async Task SetCache(List<TransportGetDTO> transports)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                    .SetPriority(CacheItemPriority.Low);

            _memoryCache.Set(cacheKey, transports, cacheEntryOptions);
        }

        public async Task<List<TransportGetDTO>> GetAsync()
        {
            List<TransportGetDTO> transports;

            if (!_memoryCache.TryGetValue(cacheKey, out transports))
            {
                transports = _mapper.Map<List<TransportGetDTO>>(
                    await _unitOfWork.TransportRepository.GetAllAsync(
                        "Make", "Model", "City", "Color", "Prices", "TransportImages"));

                await SetCache(transports);
            }

            return transports;
        }

        public async Task<TransportGetDTO> GetAsync(int id)
        {
            TransportGetDTO transport = _mapper.Map<TransportGetDTO>(
                await _unitOfWork.TransportRepository.GetAsync(x => x.Id == id,
                    "Make", "Model", "City", "Color", "Prices", "TransportImages"));

            if (transport is null)
                throw new NotFoundException(_sharedLocalizer["transportNotFound"]);

            return transport;
        }

        //public async Task CreateAsync(ColorCreateDTO colorCreateDTO)
        //{
        //    if (colorCreateDTO is null)
        //        throw new BadRequestException(_sharedLocalizer["objectIsNull"]);

        //    bool colorExists = await _unitOfWork.ColorRepository.DoesExistAsync(x =>
        //    x.NameAz.ToLower() == colorCreateDTO.NameAz.Trim().ToLower() ||
        //    x.NameRu.ToLower() == colorCreateDTO.NameRu.Trim().ToLower() ||
        //    x.NameEn.ToLower() == colorCreateDTO.NameEn.Trim().ToLower() ||
        //    x.HexCode.ToLower() == colorCreateDTO.HexCode.Trim().ToLower());

        ////    if (colorExists)
        ////        throw new RecordDuplicateException(string.Format(_sharedLocalizer["colorExists"], colorCreateDTO.NameAz, colorCreateDTO.NameRu, colorCreateDTO.NameEn, colorCreateDTO.HexCode));

        //    Color color = _mapper.Map<Color>(colorCreateDTO);

        //    await _unitOfWork.ColorRepository.AddAsync(color);
        //    await _unitOfWork.CommitAsync();
        //    await UpdateCache();
        //}
    }
}
