using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Net.Http.Headers;
using GeoFinder.Utility.Services.Interface;
using GeoFinder.Utility.Models.Request;

namespace GeoFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NominatimController : ControllerBase
    {
        private IConfiguration configuration;
        private IGeoFinderService _geoFinderService;
        public NominatimController(IConfiguration _configuration, IGeoFinderService geoFinderService)
        {
            configuration = _configuration;
            _geoFinderService = geoFinderService;
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search(string? search, string? format)
        {
            if (string.IsNullOrEmpty(search))
                throw new BadParameterException("input parameters are not correct for search");

            if (string.IsNullOrEmpty(format))
                throw new BadParameterException("input parameters are not correct for formate");
            var contentResponse = "";
            string apiEndPoint = this.configuration.GetSection("AppSettings")["NominatimAPIEndPoint"];
            var SearchUrl = string.Format(apiEndPoint + "search?q={0}&format={1}", search, format);
            var restClient = new RestClient(SearchUrl);
            var request = new RestRequest(SearchUrl, Method.Get);
            var response = await restClient.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                contentResponse = response.Content;
            }
            else
            {
                throw new HttpRequestException(response.ErrorMessage);
            }
            return Ok(contentResponse);
        }

        [HttpGet]
        [Route("Status")]
        public async Task<IActionResult> Status()
        {
            var contentResponse = "";
            string apiEndPoint = this.configuration.GetSection("AppSettings")["NominatimAPIEndPoint"];
            var statusUrl = string.Format(apiEndPoint + "status.php?format=json");
            var restClient = new RestClient(statusUrl);
            var request = new RestRequest(statusUrl, Method.Get);
            var response = await restClient.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                contentResponse = response.Content;
            }
            else
            {
                throw new HttpRequestException(response.ErrorMessage);
            }
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

            var contentResponse = "";
            string apiEndPoint = this.configuration.GetSection("AppSettings")["GetNominatimBaseURL"];
            string lookupURL = string.Format(apiEndPoint + "lookup?osm_ids={0}", osm_id);
            var restClient = new RestClient(lookupURL);
            var request = new RestRequest(lookupURL, Method.Get);
            var response = await restClient.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                contentResponse = response.Content;
            }
            else
            {
                throw new HttpRequestException(response.ErrorMessage);
            }
            return Ok(contentResponse);
        }

        [HttpPost]
        [Route("SignUp")]
        public async Task<IActionResult> SignUp(SignUpViewModel signUpViewModel)
        {
            var response = await _geoFinderService.SignUp(signUpViewModel);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }
        }
    }
}