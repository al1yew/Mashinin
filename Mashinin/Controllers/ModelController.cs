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

        //all additional methods will be here
        //return only !IsDeleted ones, or return all and show !isDeleted in front end

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

        [HttpGet("GetByMakeId/{id}")]
        public async Task<IActionResult> GetByMakeId(int id)
        {
            return Ok(await _modelService.GetByMakeIdAsync(id));
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ModelUpdateDTO modelUpdateDTO)
        {
            await _modelService.UpdateAsync(id, modelUpdateDTO);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _modelService.DeleteAsync(id);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            await _modelService.RestoreAsync(id);
            return Ok();
        }

        [HttpDelete("PermanentDelete/{id}")]
        public async Task<IActionResult> PermanentDelete(int id)
        {
            await _modelService.PermanentDelete(id);
            return Ok();
        }
    }
}
