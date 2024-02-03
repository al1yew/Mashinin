using Mashinin.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Mashinin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        public async Task<IActionResult> Index()
        {
            return Ok();
        }
    }
}
