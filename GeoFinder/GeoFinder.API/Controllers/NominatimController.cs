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
         public async Task<IActionResult> Reverse(string formate,string latitude,string longitute)
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
                  new BadParameterException("Response content Ok");
              }
              else
              {
                  throw new BadParameterException("Response content" + response);
              }
               return Ok(contentResponse);
         }
    }
}
