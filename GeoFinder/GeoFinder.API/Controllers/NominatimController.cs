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

        [HttpGet]
        public async Task<IActionResult> Search(string search, string format)
        {
            var contentResponse = "";
            string apiEndPoint = this.configuration.GetSection("AppSettings")["NominatimAPIEndPoint"];
            var searchurl = string.Format(apiEndPoint + "search?q={0}&format={1}", search, format);
            var restClient = new RestClient(searchurl);
            var request = new RestRequest(searchurl, Method.Get);
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