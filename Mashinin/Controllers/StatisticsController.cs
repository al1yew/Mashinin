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

        [HttpGet("CreateNumbers/{skip}")]
        public async Task<IActionResult> CreateNumbers(int? skip)
        {
            await _statisticsService.CreateNumbers(skip);
            return Ok();
        }

        [HttpGet("RemoveDuplicates")]
        public async Task<IActionResult> RemoveDuplicates()
        {
            await _statisticsService.RemoveDuplicates();
            return Ok();
        }

        [HttpGet("GetValuesForPython")]
        public async Task<IActionResult> GetValuesForPython()
        {
            return Ok(await _statisticsService.GetValuesForPython());
        }
    }
}
