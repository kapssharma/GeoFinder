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
            var obejctResponse = "";
            string getNominatimBaseURL = this.configuration.GetSection("AppSettings")["GetNominatimBaseURL"];
            string setNominatimParms = string.Format("lookup?osm_ids={0}", osm_id);
            getNominatimBaseURL += setNominatimParms;
            var restClient = new RestClient(getNominatimBaseURL);
            var request = new RestRequest(getNominatimBaseURL, Method.Get);
            var response = await restClient.ExecuteAsync(request);
            
            if (response.IsSuccessful)
            {

                obejctResponse = response.Content;
            }
            else
            {
                throw new HttpRequestException(response.ErrorMessage);
            }
            return Ok(obejctResponse);
        }
    }
}