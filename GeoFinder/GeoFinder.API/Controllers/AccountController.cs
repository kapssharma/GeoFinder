using GeoFinder.Utility.Models.Request;
using GeoFinder.Utility.Services.Interface;

using Microsoft.AspNetCore.Mvc;

namespace GeoFinder.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private IGeoFinderService _geoFinderService;
        public AccountController(IGeoFinderService geoFinderService)
        {
            _geoFinderService = geoFinderService;
        }
        
        [HttpPost]
        [Route("SignUp")]
        public async Task<IActionResult> SignUp(SignUpViewModel signUpViewModel)
        {
            var response = await _geoFinderService.SignUp(signUpViewModel);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> SignIn(Login SignInModel)
        {
            var response = await _geoFinderService.SignIn(SignInModel);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }
        }
    }
}
