using Mashinin.DTOs.StatisticsDTOs;

namespace Mashinin.Interfaces
{
    public interface IStatisticsService
    {
        //Task GetAveragePriceForOneCar(GetStatisticsDTO getStatisticsDTO);
        Task CreateCars();
        Task CreateNumbers(int? skip);
        Task<List<ForPythonDTO>> GetValuesForPython();
        Task RemoveDuplicates();
    }
}
