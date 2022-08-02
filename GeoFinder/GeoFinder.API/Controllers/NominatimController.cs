using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace GeoFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NominatimController : ControllerBase
    {
        private IConfiguration configuration;
        public NominatimController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        [HttpGet("{osm_id}")]
        public async Task<IActionResult> lookup(string osm_id)
        {
            var contentResponse = "";
            string apiEndPoint = this.configuration.GetSection("AppSettings")["NominatimAPIEndPoint"];
            var lookupUrl = string.Format(apiEndPoint + "lookup?osm_ids={0},", osm_id);
            var restClient = new RestClient(lookupUrl);
            var request = new RestRequest(lookupUrl, Method.Get);
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
        [Route("search")]
        public async Task<IActionResult> search(string search, string format)
        {
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
        public async Task<IActionResult> status()
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
    }
}