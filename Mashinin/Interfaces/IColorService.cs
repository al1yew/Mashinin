using Mashinin.DTOs.ColorDTOs;

namespace Mashinin.Interfaces
{
    public interface IColorService
    {
        Task<List<ColorGetDTO>> GetAsync();
        Task<ColorGetDTO> GetAsync(int id);
        Task CreateAsync(ColorCreateDTO colorCreateDTO);
        Task UpdateAsync(ColorUpdateDTO colorUpdateDTO);
        Task DeleteAsync(int id);
        Task RestoreAsync(int id);
        Task DeleteForeverAsync(int id);
    }
}
