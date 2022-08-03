using GeoFinder.API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace GeoFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NominatimController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IConfiguration configuration;
       
    public NominatimController(IConfiguration _configuration, ILogger<NominatimController> _logger)
        {
            configuration = _configuration;
            logger = _logger;

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
                logger.LogInformation(1, "Log Details.");
            }
            else
            {
                throw new HttpRequestException(response.ErrorMessage);
            }

            return Ok(contentResponse);
        }
    }
}
      
           

            