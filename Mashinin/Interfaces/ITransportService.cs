using Mashinin.DTOs.TransportDTOs;

namespace Mashinin.Interfaces
{
    public interface ITransportService
    {
        Task<List<TransportGetDTO>> GetAsync();
    }
}
