using Mashinin.DTOs.ModelDTOs;

namespace Mashinin.Interfaces
{
    public interface IModelService
    {
        Task CreateModels();
        Task<List<ModelGetDTO>> GetAsync();
        Task<List<ModelGetDTO>> GetByMakeIdAsync(int id);
        Task<ModelGetDTO> GetAsync(int id);
        Task<ModelGetDTO> GetByTurboAzIdAsync(int id);
        Task CreateAsync(ModelCreateDTO modelCreateDTO);
        Task UpdateAsync(ModelUpdateDTO modelUpdateDTO);
        Task DeleteAsync(int id);
        Task RestoreAsync(int id);
        Task DeleteForeverAsync(int id);
    }
}
