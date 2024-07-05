using UAParser;
using GeoFinder.Model;
using GeoFinder.Utility.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Net.Http.Headers;
using static System.Net.Mime.MediaTypeNames;

namespace GeoFinder.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
      public class NominatimController : Controller
      {
          private IConfiguration configuration;
        private readonly ILogService _logService;
        private readonly IGeoFinderService _geoFinderService;
        public NominatimController(IConfiguration _configuration, ILogService logService, IGeoFinderService geoFinderService)
        {
            configuration = _configuration;
            _logService = logService;
            _geoFinderService = geoFinderService;
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search(string? search, string? format)
        {
            try
            {


                if (string.IsNullOrEmpty(search))
                    throw new BadParameterException("input parameters are not correct for search");

                if (string.IsNullOrEmpty(format))
                    throw new BadParameterException("input parameters are not correct for formate");

                

                var contentResponse = "";
                string apiEndPoint = this.configuration.GetSection("AppSettings")["NominatimAPIEndPoint"];
                var searchUrl = string.Format(apiEndPoint + "search?q={0}&format={1}", search, format);
                var restClient = new RestClient(searchUrl);
                var request = new RestRequest(searchUrl, Method.Get);
                var response = await restClient.ExecuteAsync(request);

                

                if (response.IsSuccessful)
                {
                    SearchLog searched = new SearchLog();
                    var IPAddress = HttpContext.Connection.LocalIpAddress?.ToString();
                    var userAgent = HttpContext.Request.Headers["User-Agent"];
                    var uaParser = Parser.GetDefault();
                    string? BrowserType = uaParser.Parse(userAgent).UserAgent.Family;

                    await _logService.AddSearchLog(searchUrl, response.Content, apiEndPoint, search, format, BrowserType, IPAddress);

                    contentResponse = response.Content;
                }
                else
                {
                    throw new HttpRequestException(response.ErrorMessage);
                }
                return Ok(contentResponse);
            }
            catch(Exception ex)
            { 
                return BadRequest();
            }
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
        public async Task<IActionResult> Reverse(string? format, string? latitude, string? longitute)
        {
            if (string.IsNullOrEmpty(format))
                throw new BadParameterException("input parameters are not correct for format");
            if (string.IsNullOrEmpty(latitude))
                throw new BadParameterException("input parameters are not correct for latitude");
            if (string.IsNullOrEmpty(longitute))
                throw new BadParameterException("input parameters are not correct for longitute");

            var contentResponse = "";
            string apiEndPoint = this.configuration.GetSection("AppSettings")["NominatimAPIEndPoint"];
            string reverseURL = string.Format(apiEndPoint + "reverse?format={0}&lat={1}&lon={2}", format, latitude, longitute);
            var restClient = new RestClient(reverseURL);
            var request = new RestRequest(reverseURL, Method.Get);
            var response = await restClient.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                SearchLog searched = new SearchLog();
                var IPAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                var userAgent = HttpContext.Request.Headers["User-Agent"];
                var uaParser = Parser.GetDefault();
                string? BrowserType = uaParser.Parse(userAgent).UserAgent.Family;

                await _logService.AddReverseLog(reverseURL, response.Content, apiEndPoint, format, latitude, longitute, BrowserType, IPAddress);
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
                SearchLog searched = new SearchLog();
                var IPAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                var userAgent = HttpContext.Request.Headers["User-Agent"];
                var uaParser = Parser.GetDefault();
                string? BrowserType = uaParser.Parse(userAgent).UserAgent.Family;

                await _logService.AddDetailLog(lookupURL, response.Content, apiEndPoint, osm_id, BrowserType, IPAddress,"lookup");
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
        public async Task<IActionResult> Details(string? osm_id)
        {

            if (string.IsNullOrEmpty(osm_id))
                throw new BadParameterException("input parameters are not correct for osm_id");

            var contentResponse = "";
            string detailURL = string.Empty;
            string apiEndPoint = this.configuration.GetSection("AppSettings")["NominatimAPIEndPoint"];
            if (osm_id.All(Char.IsNumber))
            {
                detailURL = string.Format(apiEndPoint + "details?place_id={0}&format=json", osm_id);
            }
            else
            {
                detailURL = string.Format(apiEndPoint + "details.php?osmtype={0}&osmid={1}&format=json", osm_id.Substring(0, 1), osm_id.Remove(0, 1));
            }
            var restClient = new RestClient(detailURL);
            var request = new RestRequest(detailURL, Method.Get);
            var response = await restClient.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                SearchLog searched = new SearchLog();
                var IPAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                var userAgent = HttpContext.Request.Headers["User-Agent"];
                var uaParser = Parser.GetDefault();
                string? BrowserType = uaParser.Parse(userAgent).UserAgent.Family;
                string? IP = uaParser.Parse(IPAddress).ToString();

                await _logService.AddDetailLog(detailURL, response.Content, apiEndPoint, osm_id, BrowserType, IPAddress, "Details");
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