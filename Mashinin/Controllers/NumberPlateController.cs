using Mashinin.DTOs.NumberPlateDTOs;
using Mashinin.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Mashinin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumberPlateController : ControllerBase
    {
        private readonly INumberPlateService _numberPlateService;
        public NumberPlateController(INumberPlateService numberPlateService)
        {
            _numberPlateService = numberPlateService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _numberPlateService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _numberPlateService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(NumberPlateCreateDTO numberPlateCreateDTO)
        {
            await _numberPlateService.CreateAsync(numberPlateCreateDTO);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, NumberPlateUpdateDTO numberPlateUpdateDTO)
        {
            await _numberPlateService.UpdateAsync(id, numberPlateUpdateDTO);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _numberPlateService.DeleteAsync(id);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            await _numberPlateService.RestoreAsync(id);
            return Ok();
        }
    }
}
