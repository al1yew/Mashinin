using AutoMapper;
using Mashinin.DTOs.ColorDTOs;
using Mashinin.Entities;
using Mashinin.Exceptions;
using Mashinin.Interfaces;
using Mashinin.Localization;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;

namespace Mashinin.Implementations
{
    public class ColorService : IColorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly string cacheKey = "Colors";
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public ColorService(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache memoryCache, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _memoryCache = memoryCache;
            _sharedLocalizer = sharedLocalizer;
        }

        private async Task UpdateCache()
        {
            List<ColorGetDTO> colors = _mapper.Map<List<ColorGetDTO>>(await _unitOfWork.ColorRepository.GetAllAsync());

            await SetCache(colors);
        }
        private async Task SetCache(List<ColorGetDTO> colors)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                    .SetPriority(CacheItemPriority.Low);

            _memoryCache.Set(cacheKey, colors, cacheEntryOptions);
        }

        public async Task<List<ColorGetDTO>> GetAsync()
        {
            List<ColorGetDTO> colors;

            if (!_memoryCache.TryGetValue(cacheKey, out colors))
            {
                colors = _mapper.Map<List<ColorGetDTO>>(await _unitOfWork.ColorRepository.GetAllAsync());

                await SetCache(colors);
            }

            return colors;
        }

        public async Task<ColorGetDTO> GetAsync(int id)
        {
            ColorGetDTO color = _mapper.Map<ColorGetDTO>(await _unitOfWork.ColorRepository.GetAsync(x => x.Id == id));

            if (color is null)
                throw new NotFoundException(_sharedLocalizer["colorNotFound"]);

            return color;
        }

        public async Task CreateAsync(ColorCreateDTO colorCreateDTO)
        {
            if (colorCreateDTO is null)
                throw new BadRequestException(_sharedLocalizer["objectIsNull"]);

            bool colorExists = await _unitOfWork.ColorRepository.DoesExistAsync(x =>
            x.NameAz.ToLower() == colorCreateDTO.NameAz.Trim().ToLower() ||
            x.NameRu.ToLower() == colorCreateDTO.NameRu.Trim().ToLower() ||
            x.NameEn.ToLower() == colorCreateDTO.NameEn.Trim().ToLower() ||
            x.HexCode.ToLower() == colorCreateDTO.HexCode.Trim().ToLower());

            if (colorExists)
                throw new RecordDuplicateException(string.Format(_sharedLocalizer["colorExists"], colorCreateDTO.NameAz, colorCreateDTO.NameRu, colorCreateDTO.NameEn, colorCreateDTO.HexCode));

            Color color = _mapper.Map<Color>(colorCreateDTO);

            await _unitOfWork.ColorRepository.AddAsync(color);
            await _unitOfWork.CommitAsync();
            await UpdateCache();
        }

        public async Task UpdateAsync(ColorUpdateDTO colorUpdateDTO)
        {
            if (colorUpdateDTO is null)
                throw new BadRequestException(_sharedLocalizer["objectIsNull"]);

            //id is not the same, but values are the same
            bool colorExists = await _unitOfWork.ColorRepository.DoesExistAsync(x =>
            x.Id != colorUpdateDTO.Id &&
            (x.NameAz.ToLower() == colorUpdateDTO.NameAz.Trim().ToLower() ||
            x.NameRu.ToLower() == colorUpdateDTO.NameRu.Trim().ToLower() ||
            x.NameEn.ToLower() == colorUpdateDTO.NameEn.Trim().ToLower() ||
            x.HexCode.ToLower() == colorUpdateDTO.HexCode.Trim().ToLower()));

            if (colorExists)
                throw new RecordDuplicateException(string.Format(_sharedLocalizer["colorExists"], colorUpdateDTO.NameAz, colorUpdateDTO.NameRu, colorUpdateDTO.NameEn, colorUpdateDTO.HexCode));

            Color color = await _unitOfWork.ColorRepository.GetAsync(x => x.Id == colorUpdateDTO.Id);

            if (color is null)
                throw new NotFoundException(_sharedLocalizer["colorNotFound"]);

            color.NameAz = colorUpdateDTO.NameAz.Trim();
            color.NameRu = colorUpdateDTO.NameRu.Trim();
            color.NameEn = colorUpdateDTO.NameEn.Trim();
            color.HexCode = colorUpdateDTO.HexCode.Trim();
            color.UpdatedAt = DateTime.UtcNow.AddHours(4);
            color.IsUpdated = true;

            await _unitOfWork.CommitAsync();
            await UpdateCache();
        }

        public async Task DeleteAsync(int id)
        {
            Color color = await _unitOfWork.ColorRepository.GetAsync(x => x.Id == id);

            if (color is null)
                throw new NotFoundException(_sharedLocalizer["colorNotFound"]);

            color.IsDeleted = true;
            color.DeletedAt = DateTime.UtcNow.AddHours(4);

            await _unitOfWork.CommitAsync();
            await UpdateCache();
        }

        public async Task RestoreAsync(int id)
        {
            Color color = await _unitOfWork.ColorRepository.GetAsync(x => x.Id == id);

            if (color is null)
                throw new NotFoundException(_sharedLocalizer["colorNotFound"]);

            color.IsDeleted = false;
            color.DeletedAt = null;

            await _unitOfWork.CommitAsync();
            await UpdateCache();
        }

        public async Task DeleteForeverAsync(int id)
        {
            Color color = await _unitOfWork.ColorRepository.GetAsync(x => x.Id == id);

            if (color is null)
                throw new NotFoundException(_sharedLocalizer["colorNotFound"]);

            _unitOfWork.ColorRepository.Remove(color);
            await _unitOfWork.CommitAsync();
            await UpdateCache();
        }
    }
}
