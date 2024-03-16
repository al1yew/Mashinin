using AutoMapper;
using Mashinin.DTOs.TransportDTOs;
using Mashinin.Entities;
using Mashinin.Exceptions;
using Mashinin.Helpers;
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
        private readonly IWebHostEnvironment _env;
        public TransportService(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache memoryCache, IStringLocalizer<SharedResource> sharedLocalizer, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _memoryCache = memoryCache;
            _sharedLocalizer = sharedLocalizer;
            _env = env;
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

        public async Task CreateTransport(TransportCreateDTO transportCreateDTO)
        {
            if (transportCreateDTO is null)
                throw new BadRequestException(_sharedLocalizer["objectIsNull"]);

            Transport transport = _mapper.Map<Transport>(transportCreateDTO);

            transport.Prices.Add(new Price()
            {
                CreatedAt = DateTime.UtcNow.AddHours(4),
                Currency = transportCreateDTO.Currency,
                TransportationPrice = transportCreateDTO.TransportationPrice,
                Value = transportCreateDTO.Price,
                WinPriceMax = transportCreateDTO.WinPriceMax,
                WinPriceMin = transportCreateDTO.WinPriceMin,
            });

            List<Transport> transports = await _unitOfWork.TransportRepository.GetAllAsync();
            Transport lastTransport = transports.LastOrDefault();
            int? folderId = lastTransport?.ImagesFolderId;

            if (transportCreateDTO.FrontPhoto is not null)
            {
                transport.FrontImage = await transportCreateDTO.FrontPhoto.CreateAsync(_env, "assets", "images", "transports", folderId.ToString() ?? "1");
            }

            if (transportCreateDTO.RearPhoto is not null)
            {
                transport.RearImage = await transportCreateDTO.RearPhoto.CreateAsync(_env, "assets", "images", "transports", folderId.ToString() ?? "1");
            }

            List<TransportImage> images = new List<TransportImage>();

            foreach (IFormFile file in transportCreateDTO.Photos)
            {
                TransportImage transportImage = new TransportImage()
                {
                    CreatedAt = DateTime.UtcNow.AddHours(4),
                    Path = await file.CreateAsync(_env, "assets", "images", "transports", folderId.ToString() ?? "1"),
                };

                images.Add(transportImage);
            }

            transport.TransportImages.AddRange(images);
            await _unitOfWork.CommitAsync();
        }
    }
}
