using Mashinin.DTOs.MakeDTOs;
using Mashinin.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Mashinin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MakeController : ControllerBase
    {
        private readonly IMakeService _makeService;

        public MakeController(IMakeService makeService)
        {
            _makeService = makeService;
        }

        [HttpGet("CreateMakes")]
        public async Task<IActionResult> CreateMakes()
        {
            await _makeService.CreateMakes();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _makeService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _makeService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(MakeCreateDTO makeCreateDTO)
        {
            await _makeService.CreateAsync(makeCreateDTO);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(MakeUpdateDTO makeUpdateDTO)
        {
            await _makeService.UpdateAsync(makeUpdateDTO);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _makeService.DeleteAsync(id);
            return Ok();
        }

        [HttpOptions("{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            await _makeService.RestoreAsync(id);
            return Ok();
        }

        [HttpHead("{id}")]
        public async Task<IActionResult> Head(int id)
        {
            await _makeService.DeleteForeverAsync(id);
            return Ok();
        }
    }
}
