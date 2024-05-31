using Mashinin.DTOs.ModelDTOs;

namespace Mashinin.Interfaces
{
    public interface IModelService
    {
        Task<List<ModelGetDTO>> GetAsync();
        Task<List<ModelGetDTO>> GetByMakeIdAsync(int id);
        Task<ModelGetDTO> GetAsync(int id);
        Task<ModelGetDTO> GetByTurboAzIdAsync(int id);
        Task CreateAsync(ModelCreateDTO modelCreateDTO);
        Task UpdateAsync(int id, ModelUpdateDTO modelUpdateDTO);
        Task DeleteAsync(int id);
        Task RestoreAsync(int id);
        Task PermanentDelete(int id);
    }
}
