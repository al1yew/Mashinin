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

        [HttpGet("CreateCars")]
        public async Task<IActionResult> CreateCars()
        {
            await _statisticsService.CreateCars();
            return Ok();
        }

        [HttpGet("CreateNumbers")]
        public async Task<IActionResult> CreateNumbers()
        {
            await _statisticsService.CreateNumbers();
            return Ok();
        }
    }
}
