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

        [HttpPut]
        public async Task<IActionResult> Put(ColorUpdateDTO colorUpdateDTO)
        {
            await _colorService.UpdateAsync(colorUpdateDTO);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _colorService.DeleteAsync(id);
            return Ok();
        }

        [HttpOptions("{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            await _colorService.RestoreAsync(id);
            return Ok();
        }

        [HttpHead("{id}")] // does not return exception message
        public async Task<IActionResult> Head(int id)
        {
            await _colorService.DeleteForeverAsync(id);
            return Ok();
        }
    }
}
