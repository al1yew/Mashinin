using AutoMapper;
using Mashinin.DTOs.MakeDTOs;
using Mashinin.Entities;
using Mashinin.Exceptions;
using Mashinin.Interfaces;
using Mashinin.Localization;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;

namespace Mashinin.Implementations
{
    public class MakeService : IMakeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly string cacheKey = "Makes";
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public MakeService(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache memoryCache, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _memoryCache = memoryCache;
            _sharedLocalizer = sharedLocalizer;
        }

        private async Task<List<MakeGetDTO>> RetrieveMakes()
        {
            return await _unitOfWork.MakeRepository.GetFilteredAsync(
                x => new MakeGetDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    TurboAzId = x.TurboAzId,
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
            List<MakeGetDTO> makes = await RetrieveMakes();

            await SetCache(makes);
        }

        private async Task SetCache(List<MakeGetDTO> makes)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromDays(1))
                    .SetPriority(CacheItemPriority.Low);

            _memoryCache.Set(cacheKey, makes, cacheEntryOptions);
        }

        public async Task<List<MakeGetDTO>> GetAsync()
        {
            List<MakeGetDTO> makes;

            if (!_memoryCache.TryGetValue(cacheKey, out makes))
            {
                makes = await RetrieveMakes();

                await SetCache(makes);
            }

            return makes;
        }

        public async Task<MakeGetDTO> GetAsync(int id)
        {
            List<MakeGetDTO> makes = await GetAsync();
            MakeGetDTO make = makes.FirstOrDefault(x => x.Id == id);

            if (make is null)
                throw new NotFoundException(_sharedLocalizer["makeNotFound"]);

            return make;
        }

        public async Task<MakeGetDTO> GetByTurboAzIdAsync(int id)
        {
            List<MakeGetDTO> makes = await GetAsync();
            MakeGetDTO make = makes.FirstOrDefault(x => x.TurboAzId == id);

            if (make is null)
                throw new NotFoundException(_sharedLocalizer["makeNotFound"]);

            return make;
        }

        public async Task CreateAsync(MakeCreateDTO makeCreateDTO)
        {
            if (makeCreateDTO is null)
                throw new BadRequestException(_sharedLocalizer["objectIsNull"]);

            bool makeExists = await _unitOfWork.MakeRepository.DoesExistAsync(x =>
            x.Name.ToLower() == makeCreateDTO.Name.Trim().ToLower() ||
            x.TurboAzId == makeCreateDTO.TurboAzId);

            if (makeExists)
                throw new RecordDuplicateException(
                    string.Format(_sharedLocalizer["makeExists"],
                    makeCreateDTO.Name,
                    makeCreateDTO.TurboAzId)
                    );

            Make make = _mapper.Map<Make>(makeCreateDTO);

            await _unitOfWork.MakeRepository.AddAsync(make);
            await _unitOfWork.CommitAsync();
            await UpdateCache();
        }

        public async Task UpdateAsync(int id, MakeUpdateDTO makeUpdateDTO)
        {
            if (id != makeUpdateDTO.Id)
                throw new BadRequestException(_sharedLocalizer["idsAreDifferent"]);

            if (makeUpdateDTO is null)
                throw new BadRequestException(_sharedLocalizer["objectIsNull"]);

            bool makeExists = await _unitOfWork.MakeRepository.DoesExistAsync(x =>
            x.Id != makeUpdateDTO.Id &&
            (x.Name.ToLower() == makeUpdateDTO.Name.Trim().ToLower() ||
            x.TurboAzId == makeUpdateDTO.TurboAzId));

            if (makeExists)
                throw new RecordDuplicateException(
                    string.Format(_sharedLocalizer["makeExists"],
                    makeUpdateDTO.Name,
                    makeUpdateDTO.TurboAzId)
                    );

            Make make = await _unitOfWork.MakeRepository.GetAsync(x => x.Id == makeUpdateDTO.Id);

            if (make is null)
                throw new NotFoundException(_sharedLocalizer["makeNotFound"]);

            make.Name = makeUpdateDTO.Name.Trim();
            make.TurboAzId = makeUpdateDTO.TurboAzId;
            make.UpdatedAt = DateTime.UtcNow.AddHours(4);
            make.IsUpdated = true;

            await _unitOfWork.CommitAsync();
            await UpdateCache();
        }

        public async Task DeleteAsync(int id)
        {
            Make make = await _unitOfWork.MakeRepository.GetAsync(x => x.Id == id);

            if (make is null)
                throw new NotFoundException(_sharedLocalizer["makeNotFound"]);

            make.IsDeleted = true;
            make.DeletedAt = DateTime.UtcNow.AddHours(4);

            await _unitOfWork.CommitAsync();
            await UpdateCache();
        }

        public async Task RestoreAsync(int id)
        {
            Make make = await _unitOfWork.MakeRepository.GetAsync(x => x.Id == id);

            if (make is null)
                throw new NotFoundException(_sharedLocalizer["makeNotFound"]);

            make.IsDeleted = false;
            make.DeletedAt = null;

            await _unitOfWork.CommitAsync();
            await UpdateCache();
        }

        public async Task PermanentDelete(int id)
        {
            Make make = await _unitOfWork.MakeRepository.GetAsync(x => x.Id == id);

            if (make is null)
                throw new NotFoundException(_sharedLocalizer["makeNotFound"]);

            _unitOfWork.MakeRepository.Remove(make);
            await _unitOfWork.CommitAsync();
            await UpdateCache();
        }
    }
}
