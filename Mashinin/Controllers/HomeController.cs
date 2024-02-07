using Mashinin.Enums;
using Mashinin.Interfaces;
using Mashinin.Localization.EnumLocalizers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Net;

namespace Mashinin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHomeService _homeService;
        private readonly IStringLocalizer<FuelType> _localizer;

        public HomeController(IHomeService homeService, IStringLocalizer<FuelType> localizer)
        {
            _homeService = homeService;
            _localizer = localizer;
        }

        public async Task<IActionResult> Index()
        {
            return Ok();
        }

        [HttpGet("GetEnums")]
        public IActionResult GetEnums()
        {
            var fuelTypes = Enum.GetValues(typeof(FuelTypes))
                                .Cast<FuelTypes>()
                                .Select(fuelType => new
                                {
                                    Value = (int)fuelType,
                                    Name = _localizer[fuelType.ToString()].Value
                                })
                                .ToList();
            //dla druqix enumov ne budu delat uje resx file, i tak ne ponadobitsa. vse budet hard written in constant files
            return Ok(fuelTypes);
        }
    }
}
