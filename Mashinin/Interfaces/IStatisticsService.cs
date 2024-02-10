using Mashinin.DTOs.StatisticsDTOs;

namespace Mashinin.Interfaces
{
    public interface IStatisticsService
    {
        Task<List<ExtractedCarPriceDetailsDTO>> GetAveragePriceForOneCar(GetStatisticsDTO getStatisticsDTO);
    }
}
