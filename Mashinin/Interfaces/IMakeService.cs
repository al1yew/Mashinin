using Mashinin.DTOs.MakeDTOs;

namespace Mashinin.Interfaces
{
    public interface IMakeService
    {
        Task CreateMakes();
        Task<List<MakeGetDTO>> GetAsync();
        Task<MakeGetDTO> GetAsync(int id);
        Task<MakeGetDTO> GetByTurboAzIdAsync(int id);
        Task CreateAsync(MakeCreateDTO makeCreateDTO);
        Task UpdateAsync(MakeUpdateDTO makeUpdateDTO);
        Task DeleteAsync(int id);
        Task RestoreAsync(int id);
        Task DeleteForeverAsync(int id);
    }
}
