using Mashinin.DTOs.MakeDTOs;

namespace Mashinin.Interfaces
{
    public interface IMakeService
    {
        Task CreateMakes();
        Task<List<MakeGetDTO>> GetAsync();
        Task<MakeGetDTO> GetAsync(int id);
    }
}
