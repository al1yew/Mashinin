using Microsoft.AspNetCore.Mvc;

namespace Mashinin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
