using AutoMapper;
using Mashinin.DTOs.MakeDTOs;
using Mashinin.Entities;
using Mashinin.Exceptions;
using Mashinin.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Mashinin.Implementations
{
    public class MakeService : IMakeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly string cacheKey = "Makes";
        public MakeService(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache memoryCache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public async Task CreateMakes()
        {
            //string json = "find in txt";

            //List<Make> makes = new List<Make>();

            //makes = JsonConvert.DeserializeObject<List<Make>>(json);

            //foreach (Make make in makes)
            //{
            //    make.CreatedAt = DateTime.UtcNow.AddHours(4);
            //}

            //await _unitOfWork.MakeRepository.AddRangeAsync(makes);
            //await _unitOfWork.CommitAsync();
        }
        private async Task UpdateCache()
        {
            List<MakeGetDTO> makes = _mapper.Map<List<MakeGetDTO>>(await _unitOfWork.MakeRepository.GetAllAsync());

            await SetCache(makes);
        }
        private async Task SetCache(List<MakeGetDTO> makes)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                    .SetPriority(CacheItemPriority.Low);

            _memoryCache.Set(cacheKey, makes, cacheEntryOptions);
        }

        public async Task<List<MakeGetDTO>> GetAsync()
        {
            List<MakeGetDTO> makes;

            if (!_memoryCache.TryGetValue(cacheKey, out makes))
            {
                makes = _mapper.Map<List<MakeGetDTO>>(await _unitOfWork.MakeRepository.GetAllAsync());

                await SetCache(makes);
            }

            return makes;
        }

        public async Task<MakeGetDTO> GetAsync(int id)
        {
            MakeGetDTO make = _mapper.Map<MakeGetDTO>(await _unitOfWork.MakeRepository.GetAsync(x => x.Id == id, "Models"));

            if (make is null)
                throw new NotFoundException("Make is not found.");

            return make;
        }

        public async Task<MakeGetDTO> GetByTurboAzIdAsync(int id)
        {
            MakeGetDTO make = _mapper.Map<MakeGetDTO>(await _unitOfWork.MakeRepository.GetAsync(x => x.TurboAzId == id, "Models"));

            if (make is null)
                throw new NotFoundException("Make is not found.");

            return make;
        }

        public async Task CreateAsync(MakeCreateDTO makeCreateDTO)
        {
            if (makeCreateDTO is null)
                throw new BadRequestException("makeCreateDTO is null");

            bool makeExists = await _unitOfWork.MakeRepository.DoesExistAsync(x =>
            x.Name.ToLower() == makeCreateDTO.Name.Trim().ToLower() &&
            x.TurboAzId == makeCreateDTO.TurboAzId);

            if (makeExists)
                throw new RecordDuplicateException($"Make exists with Name {makeCreateDTO.Name} and TurboAzId {makeCreateDTO.TurboAzId}");

            Make make = _mapper.Map<Make>(makeCreateDTO);

            await _unitOfWork.MakeRepository.AddAsync(make);
            await _unitOfWork.CommitAsync();
            await UpdateCache();
        }

        public async Task UpdateAsync(MakeUpdateDTO makeUpdateDTO)
        {
            if (makeUpdateDTO is null)
                throw new BadRequestException("makeUpdateDTO is null");

            //id is not the same, but values are the same
            bool makeExists = await _unitOfWork.MakeRepository.DoesExistAsync(x =>
            x.Id != makeUpdateDTO.Id &&
            (x.Name.ToLower() == makeUpdateDTO.Name.Trim().ToLower() &&
            x.TurboAzId == makeUpdateDTO.TurboAzId));

            if (makeExists)
                throw new RecordDuplicateException($"Make exists with Name {makeUpdateDTO.Name} and TurboAzId {makeUpdateDTO.TurboAzId}");

            Make make = await _unitOfWork.MakeRepository.GetAsync(x => x.Id == makeUpdateDTO.Id);

            if (make is null)
                throw new NotFoundException("Make is not found.");

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
                throw new NotFoundException("Make is not found.");

            make.IsDeleted = true;
            make.DeletedAt = DateTime.UtcNow.AddHours(4);

            await _unitOfWork.CommitAsync();
            await UpdateCache();
        }

        public async Task RestoreAsync(int id)
        {
            Make make = await _unitOfWork.MakeRepository.GetAsync(x => x.Id == id);

            if (make is null)
                throw new NotFoundException("Make is not found.");

            make.IsDeleted = false;
            make.DeletedAt = null;

            await _unitOfWork.CommitAsync();
            await UpdateCache();
        }

        public async Task DeleteForeverAsync(int id)
        {
            Make make = await _unitOfWork.MakeRepository.GetAsync(x => x.Id == id);

            if (make is null)
                throw new NotFoundException("Make is not found.");

            _unitOfWork.MakeRepository.Remove(make);
            await _unitOfWork.CommitAsync();
            await UpdateCache();
        }
    }
}
