using Mashinin.DTOs.NumberPlateDTOs;

namespace Mashinin.Interfaces
{
    public interface INumberPlateService
    {
        Task<List<NumberPlateGetDTO>> GetAsync();
        Task<NumberPlateGetDTO> GetAsync(int id);
        Task CreateAsync(NumberPlateCreateDTO numberPlateCreateDTO);
        Task UpdateAsync(int id, NumberPlateUpdateDTO numberPlateUpdateDTO);
        Task DeleteAsync(int id);
        Task RestoreAsync(int id);
    }
}
