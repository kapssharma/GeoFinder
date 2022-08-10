using GeoFinder.API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using GeoFinder.API.Enum;

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

        [HttpGet]
        [Route("Search")]

        public async Task<IActionResult> Search(string? search, FormatTypeEnum format)


        {
            var contentResponse = "";
            try
            {
                if (string.IsNullOrEmpty(search))
                    throw new BadParameterException("input parameters are not correct for search");
                string apiEndPoint = this.configuration.GetSection("AppSettings")["NominatimAPIEndPoint"];
                logger.LogInformation("API end point" + apiEndPoint);
                var SearchUrl = string.Format(apiEndPoint + "search?q={0}&format={1}", search, format);
                logger.LogInformation("API end point" + SearchUrl);
                var restClient = new RestClient(SearchUrl);
                var request = new RestRequest(SearchUrl, Method.Get);
                var response = await restClient.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    contentResponse = response.Content;
                    logger.LogInformation(contentResponse);
                }

                else
                {
                    logger.LogError("Bad request" + response);
                    throw new HttpRequestException(response.ErrorMessage);
                }

            }
            catch (Exception ex)
            {
                logger.LogError("Response Content Error" + ex.Message);
                throw new Exception(ex.Message);

            }
            return Ok(contentResponse);
        }
        [HttpGet]
        [Route("Status")]
        public async Task<IActionResult> Status()
        {
            var contentResponse = "";
            try
            {
                string apiEndPoint = this.configuration.GetSection("AppSettings")["NominatimAPIEndPoint"];
                logger.LogInformation("API end point" + apiEndPoint);
                var statusUrl = string.Format(apiEndPoint + "status.php?format=json");
                logger.LogInformation("API end point" + statusUrl);
                var restClient = new RestClient(statusUrl);
                var request = new RestRequest(statusUrl, Method.Get);
                var response = await restClient.ExecuteAsync(request);
                if (response.IsSuccessful)
                {
                    contentResponse = response.Content;
                    logger.LogInformation(contentResponse);
                }
                else
                {
                    logger.LogError("Bad request" + response);
                    throw new HttpRequestException(response.ErrorMessage);
                }

            }
            catch (Exception ex)
            {
                logger.LogError("Response Content Error" + ex.Message);

                throw new Exception(ex.Message);
            }
            return Ok(contentResponse);

        }

        [HttpGet]
        [Route("Reverse")]
        public async Task<IActionResult> Reverse(string? latitude, string? longitute, FormatTypeEnum format)
        {
            var contentResponse = "";
            try
            {
                if (string.IsNullOrEmpty(latitude))
                    throw new BadParameterException("input parameters are not correct for latitude");
                if (string.IsNullOrEmpty(longitute))
                    throw new BadParameterException("input parameters are not correct for longitute");

                string apiEndPoint = this.configuration.GetSection("AppSettings")["NominatimAPIEndPoint"];
                logger.LogInformation("API end point" + apiEndPoint);
                string reverseURL = string.Format(apiEndPoint + "reverse?lat={0}&lon={1}&format={2}", latitude, longitute, format);
                logger.LogInformation("API end point" + reverseURL);
                var restClient = new RestClient(reverseURL);
                var request = new RestRequest(reverseURL, Method.Get);
                var response = await restClient.ExecuteAsync(request);


                if (response.IsSuccessful)
                {
                    contentResponse = response.Content;
                    logger.LogInformation(contentResponse);
                }

                else
                {
                    logger.LogError("Bad request" + response);
                    throw new APIException(response.ErrorMessage);
                }

            }
            catch (Exception ex)
            {
                logger.LogError("Response Content Error" + ex.Message);
                throw new Exception(ex.Message);

            }
            return Ok(contentResponse);
        }

        [HttpGet]
        [Route("Lookup")]

        public async Task<IActionResult> lookup(string osm_id, FormatTypeEnum format)
        {
            var contentResponse = "";
            try
            {
                string apiEndPoint = this.configuration.GetSection("AppSettings")["NominatimAPIEndPoint"];
                logger.LogInformation("API end point" + apiEndPoint);
                var lookupUrl = string.Format(apiEndPoint + "lookup?osm_ids={0}&format={1}", osm_id, format);
                var restClient = new RestClient(lookupUrl);
                logger.LogInformation("API end point" + lookupUrl);
                var request = new RestRequest(lookupUrl, Method.Get);
                var response = await restClient.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    contentResponse = response.Content;
                    logger.LogInformation(contentResponse);
                }

                else
                {
                    logger.LogError("Bad request" + response);
                    throw new HttpRequestException(response.ErrorMessage);
                }

            }
            catch (Exception ex)
            {
                logger.LogError("Response Content Error" + ex.Message);
                throw new Exception(ex.Message);

            }
            return Ok(contentResponse);
        }
    }
}
