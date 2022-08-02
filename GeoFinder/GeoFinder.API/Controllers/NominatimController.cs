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
        public async Task<IActionResult> status(string format)
        {
            var contentResponse = "";
            string apiEndPoint = this.configuration.GetSection("AppSettings")["NominatimAPIEndPoint"];
            var lookupUrl = string.Format(apiEndPoint + "status.php?format={0}", format);
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
    }
}