using Mashinin.DTOs.StatisticsDTOs;
using Mashinin.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Mashinin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpPost("GetAveragePriceForOneCar")]
        public async Task<IActionResult> GetAveragePriceForOneCar(GetStatisticsDTO getStatisticsDTO)
        {
            return Ok(await _statisticsService.GetAveragePriceForOneCar(getStatisticsDTO));
        }
    }
}
