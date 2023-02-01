using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Net.Http.Headers;

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
            string apiEndPoint = this.configuration.GetSection("AppSettings")["NominatimAPIEndPoint"];
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
        [HttpGet]
        [Route("Details")]
        public async Task<IActionResult> Deatails(string? osm_id, bool isThisPlaceID)
        {

            if (string.IsNullOrEmpty(osm_id))
                throw new BadParameterException("input parameters are not correct for osm_id");

            var contentResponse = "";
            string lookupURL = string.Empty;
            string osm_type = string.Empty;
            string osmid = string.Empty;
            string apiEndPoint = this.configuration.GetSection("AppSettings")["NominatimAPIEndPoint"];
            if (isThisPlaceID)
            {
                lookupURL = string.Format(apiEndPoint + "details?place_id={0}&format=json", osm_id);
            }
            else
            {
                osm_type = osm_id.Substring(0, 1);
                osmid = osm_id.Remove(0, 1);
                lookupURL = string.Format(apiEndPoint + "details.php?osmtype={0}&osmid={1}&format=json", osm_type, osmid);
            }

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