using Mashinin.DTOs.CityDTOs;
using Mashinin.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Mashinin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet("CreateCities")]
        public async Task<IActionResult> CreateCities()
        {
            await _cityService.CreateCities();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _cityService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _cityService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CityCreateDTO cityCreateDTO)
        {
            await _cityService.CreateAsync(cityCreateDTO);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(CityUpdateDTO cityUpdateDTO)
        {
            await _cityService.UpdateAsync(cityUpdateDTO);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _cityService.DeleteAsync(id);
            return Ok();
        }

        [HttpOptions("{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            await _cityService.RestoreAsync(id);
            return Ok();
        }

        [HttpHead("{id}")] // does not return exception message
        public async Task<IActionResult> Head(int id)
        {
            await _cityService.DeleteForeverAsync(id);
            return Ok();
        }

    }
}
