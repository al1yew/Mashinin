using AutoMapper;
using Mashinin.DTOs.CityDTOs;
using Mashinin.DTOs.ColorDTOs;
using Mashinin.Entities;
using Mashinin.Exceptions;
using Mashinin.Interfaces;
using Mashinin.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using System.Globalization;

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

        private async Task<List<ColorGetDTO>> RetrieveColors()
        {
            return await _unitOfWork.ColorRepository.GetFilteredAsync(
                x => new ColorGetDTO
                {
                    Id = x.Id,
                    Name = EF.Property<string>(x, "Name" + CultureInfo.CurrentCulture.EnglishName.Substring(0, 2)),
                    CreatedAt = x.CreatedAt.Value.ToString("dd.MM.yyyy HH:mm:ss"),
                    DeletedAt = x.DeletedAt.Value.ToString("dd.MM.yyyy HH:mm:ss"),
                    IsDeleted = x.IsDeleted,
                    IsUpdated = x.IsUpdated,
                    HexCode = x.HexCode,
                    UpdatedAt = x.UpdatedAt.Value.ToString("dd.MM.yyyy HH:mm:ss")
                }
            );
        }

        private async Task UpdateCache()
        {
            List<ColorGetDTO> colors = await RetrieveColors();

            await SetCache(colors);
        }

        private async Task SetCache(List<ColorGetDTO> colors)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromDays(1))
                    .SetPriority(CacheItemPriority.Low);

            _memoryCache.Set(cacheKey, colors, cacheEntryOptions);
        }

        public async Task<List<ColorGetDTO>> GetAsync()
        {
            List<ColorGetDTO> colors;

            if (!_memoryCache.TryGetValue(cacheKey, out colors))
            {
                colors = await RetrieveColors();

                await SetCache(colors);
            }

            return colors;
        }

        public async Task<ColorGetDTO> GetAsync(int id)
        {
            List<ColorGetDTO> colors = await GetAsync();
            ColorGetDTO color = colors.FirstOrDefault(x => x.Id == id);

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
                throw new RecordDuplicateException(
                    string.Format(_sharedLocalizer["colorExists"],
                    colorCreateDTO.NameAz,
                    colorCreateDTO.NameRu,
                    colorCreateDTO.NameEn,
                    colorCreateDTO.HexCode)
                    );

            Color color = _mapper.Map<Color>(colorCreateDTO);

            await _unitOfWork.ColorRepository.AddAsync(color);
            await _unitOfWork.CommitAsync();
            await UpdateCache();
        }

        public async Task UpdateAsync(int id, ColorUpdateDTO colorUpdateDTO)
        {
            if (id != colorUpdateDTO.Id)
                throw new BadRequestException(_sharedLocalizer["idsAreDifferent"]);

            if (colorUpdateDTO is null)
                throw new BadRequestException(_sharedLocalizer["objectIsNull"]);

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

        public async Task PermanentDelete(int id)
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
