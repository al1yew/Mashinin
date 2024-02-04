using Mashinin.DTOs.ModelDTOs;
using Mashinin.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Mashinin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        private readonly IModelService _modelService;

        public ModelController(IModelService modelService)
        {
            _modelService = modelService;
        }

        [HttpGet("CreateModels")]
        public async Task<IActionResult> CreateModels()
        {
            await _modelService.CreateModels();

            return Ok();
        }

        [HttpGet("GetByMakeId/{id}")]
        public async Task<IActionResult> GetByMakeId(int id)
        {
            return Ok(await _modelService.GetByMakeIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _modelService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _modelService.GetAsync(id));
        }

        [HttpGet("GetByTurboAzId/{id}")]
        public async Task<IActionResult> GetByTurboAzId(int id)
        {
            return Ok(await _modelService.GetByTurboAzIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(ModelCreateDTO modelCreateDTO)
        {
            await _modelService.CreateAsync(modelCreateDTO);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(ModelUpdateDTO modelUpdateDTO)
        {
            await _modelService.UpdateAsync(modelUpdateDTO);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _modelService.DeleteAsync(id);
            return Ok();
        }

        [HttpOptions("{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            await _modelService.RestoreAsync(id);
            return Ok();
        }

        [HttpHead("{id}")] // does not return exception message
        public async Task<IActionResult> Head(int id)
        {
            await _modelService.DeleteForeverAsync(id);
            return Ok();
        }
    }
}
