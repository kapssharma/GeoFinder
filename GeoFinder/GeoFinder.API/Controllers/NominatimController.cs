using GeoFinder.Data;
using GeoFinder.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace GeoFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(CustomAuthorization))]
    public class NominatimController : ControllerBase
    {
        private ApplicationDbContext  context;
        private IConfiguration configuration;
        public NominatimController(IConfiguration _configuration, ApplicationDbContext _context )
        {
            configuration = _configuration;
            context = _context; 
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search(string? search, string? format)
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
            var contentResponse = "";
            string apiEndPoint = this.configuration.GetSection("AppSettings")["NominatimAPIEndPoint"];
            string reverseURL = string.Format(apiEndPoint + "reverse?format={0}&lat={1}&lon={2}", formate, latitude, longitute);
            var restClient = new RestClient(reverseURL);
            var request = new RestRequest(reverseURL, Method.Get);
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
        [Route("Lookup")]
        public async Task<IActionResult> Lookup(string? osm_id)
        {
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

    }
}



