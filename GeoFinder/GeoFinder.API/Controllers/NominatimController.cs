using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Net.Http.Headers;
using GeoFinder.Utility.Services.Interface;
using GeoFinder.Utility.Models.Request;
using GeoFinder.Utility.Models.Response;
using GeoFinder.Utility.Enum;

namespace GeoFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class NominatimController : ControllerBase
    {
        private readonly ILogger<NominatimController> _logger;
        private IConfiguration configuration;
        private IGeoFinderService _geoFinderService;
        public NominatimController(ILogger<NominatimController> logger, IConfiguration _configuration, IGeoFinderService geoFinderService)
        {
            _logger = logger;
            configuration = _configuration;
            _geoFinderService = geoFinderService;
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search(string? search, Format format)
        {
            try
            {
                var contentResponse = await _geoFinderService.Search(search, GeoFinder.Utility.Classes.EnumExtension.GetEnumDescription(format));

                return Ok(contentResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Search() : {ex.Message}",
                         DateTime.UtcNow.ToLongTimeString());
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Status")]
        public async Task<IActionResult> Status()
        {
            var contentResponse = await _geoFinderService.Status();
            return Ok(contentResponse);
        }


        [HttpGet]
        [Route("Reverse")]
        public async Task<IActionResult> Reverse(string? formate, string? latitude, string? longitute)
        {
            if (string.IsNullOrEmpty(formate))
                throw new BadParameterException("input parameters are not correct for formate");
            if (string.IsNullOrEmpty(latitude))
                throw new BadParameterException("input parameters are not correct for latitude");
            if (string.IsNullOrEmpty(longitute))
                throw new BadParameterException("input parameters are not correct for longitute");

            var contentResponse = "";
            string apiEndPoint = this.configuration.GetSection("AppSettings")["NominatimAPIEndPoint"];
            string reverseURL = string.Format(apiEndPoint + "reverse?format={0}&lat={1}&lon={2}", formate, latitude, longitute);
            var restClient = new RestClient(reverseURL);
            var request = new RestRequest(reverseURL, Method.Get);
            var response = await restClient.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                contentResponse = response.Content;
                new BadParameterException("Response content Ok");
            }
            else
            {
                throw new HttpRequestException(response.ErrorMessage);
            }
            return Ok(contentResponse);
        }

        [HttpGet]
        [Route("Lookup")]
        public async Task<IActionResult> Lookup(string? osm_id)
        {
            if (string.IsNullOrEmpty(osm_id))
                throw new BadParameterException("input parameters are not correct for osm_id");
            var contentResponse = await _geoFinderService.LookUp(osm_id);
            return Ok(contentResponse);
        }

    }

}
