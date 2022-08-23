using GeoFinder.Utility.Services.Interface;

using Microsoft.AspNetCore.Mvc;

namespace GeoFinder.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AreaController : Controller
    {
       
        private IGeoFinderService _geoFinderService;
        public AreaController (IGeoFinderService geoFinderService)
        {
            _geoFinderService = geoFinderService;
        }
        [HttpGet]
        public async Task<IActionResult> getCountries()
        {
            var response = await _geoFinderService.getCountries();
            return Ok(response);
        }
    }
}
