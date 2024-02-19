using HtmlAgilityPack;
using Mashinin.DTOs.StatisticsDTOs;
using Mashinin.Entities;

namespace Mashinin.Interfaces
{
    public interface IStatisticsService
    {
        //Task GetAveragePriceForOneCar(GetStatisticsDTO getStatisticsDTO);
        Task CreateCars();
        Task CreateNumbers();
    }
}
