using Mashinin.Interfaces;
using Mashinin.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace Mashinin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportController : ControllerBase
    {
        private readonly ITransportService _transportService;
        public TransportController(ITransportService transportService)
        {
            _transportService = transportService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _transportService.GetAsync());
        }
    }
}
