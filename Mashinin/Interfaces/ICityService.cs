using Mashinin.DTOs.CityDTOs;

namespace Mashinin.Interfaces
{
    public interface ICityService
    {
        Task<List<CityGetDTO>> GetAsync();
        Task<CityGetDTO> GetAsync(int id);
        Task CreateAsync(CityCreateDTO cityCreateDTO);
        Task UpdateAsync(int id, CityUpdateDTO cityUpdateDTO);
        Task DeleteAsync(int id);
        Task RestoreAsync(int id);
        Task PermanentDelete(int id);
    }
}
