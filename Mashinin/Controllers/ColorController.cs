using Mashinin.DTOs.ColorDTOs;
using Mashinin.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Mashinin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }

        //all additional methods will be here
        //return only !IsDeleted ones, or return all and show !isDeleted in front end

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _colorService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _colorService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(ColorCreateDTO colorCreateDTO)
        {
            await _colorService.CreateAsync(colorCreateDTO);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ColorUpdateDTO colorUpdateDTO)
        {
            await _colorService.UpdateAsync(id, colorUpdateDTO);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _colorService.DeleteAsync(id);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            await _colorService.RestoreAsync(id);
            return Ok();
        }

        [HttpDelete("PermanentDelete/{id}")]
        public async Task<IActionResult> PermanentDelete(int id)
        {
            await _colorService.PermanentDelete(id);
            return Ok();
        }
    }
}
