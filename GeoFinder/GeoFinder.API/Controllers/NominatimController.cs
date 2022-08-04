using GeoFinder.Model;
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
        [HttpGet]
        [Route("Authenticate")]
        public ActionResult Authenticate()
        {
            var userList = Getuserlist();
            var User_API_Tokenlist = GetUser_API_Tokenlist();
            Request.Headers.Add("Authorization", "11223344-5566-7788-99AA-BBCCDDEEFF99");
            string token = HttpContext.Request.Headers["Authorization"];
            bool isExist = User_API_Tokenlist.Select(x => x.Id == Guid.Parse(token)).FirstOrDefault();
            if (isExist)
            {
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }
        [HttpGet]
        [Route("UserList")]
        public List<Users> Getuserlist()
        {
            List<Users> authors = new List<Users>
        {
            new Users { Id= new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00"), Name = "Ramzan", EmailAddress = "Ramzan567@gmail.com" },
        };
            return authors;
        }

        [HttpGet]
        [Route("TokenListList")]
        public List<User_API_Token> GetUser_API_Tokenlist()
        {
            List<User_API_Token> User_API_Token = new List<User_API_Token>
           {
             new User_API_Token { Id= new Guid("11223344-5566-7788-99AA-BBCCDDEEFF99"), UserId= new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00") },
           };
            return User_API_Token;
        }
    }
}


