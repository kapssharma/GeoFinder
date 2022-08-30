using GeoFinder.Utility.Services.Interface;

using Microsoft.AspNetCore.Mvc;

namespace GeoFinder.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AreaController : Controller
    {
        private IGeoFinderService _geoFinderService;
        public AreaController (IGeoFinderService geoFinderService)
        {
            _geoFinderService = geoFinderService;
        }
        [HttpGet]
        [Route("GetCountries")]
        public async Task<IActionResult> GetCountries()
        {
            var response = await _geoFinderService.GetCountries();
            return Ok(response);
        }

        [HttpGet]
        [Route("GetState")]
        public async Task<IActionResult> GetState(string countryID)
        {
            var response = await _geoFinderService.GetState(countryID);
            return Ok(response);
        }
    }
}
