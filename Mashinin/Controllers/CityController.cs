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

        //all additional methods will be here
        //return only !SsDeleted ones, or return all and show !isDeleted in front end


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

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CityUpdateDTO cityUpdateDTO)
        {
            await _cityService.UpdateAsync(id, cityUpdateDTO);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _cityService.DeleteAsync(id);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            await _cityService.RestoreAsync(id);
            return Ok();
        }

        [HttpDelete("PermanentDelete/{id}")]
        public async Task<IActionResult> PermanentDelete(int id)
        {
            await _cityService.PermanentDelete(id);
            return Ok();
        }

    }
}
