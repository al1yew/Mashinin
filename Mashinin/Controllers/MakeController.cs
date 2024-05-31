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

        //all additional methods will be here
        //return only !IsDeleted ones, or return all and show !isDeleted in front end

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

        [HttpGet("GetByTurboAzId/{id}")]
        public async Task<IActionResult> GetByTurboAzId(int id)
        {
            return Ok(await _makeService.GetByTurboAzIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(MakeCreateDTO makeCreateDTO)
        {
            await _makeService.CreateAsync(makeCreateDTO);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, MakeUpdateDTO makeUpdateDTO)
        {
            await _makeService.UpdateAsync(id, makeUpdateDTO);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _makeService.DeleteAsync(id);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            await _makeService.RestoreAsync(id);
            return Ok();
        }

        [HttpDelete("PermanentDelete/{id}")]
        public async Task<IActionResult> PermanentDelete(int id)
        {
            await _makeService.PermanentDelete(id);
            return Ok();
        }
    }
}
