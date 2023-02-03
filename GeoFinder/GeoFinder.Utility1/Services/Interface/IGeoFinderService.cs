using GeoFinder.Utility1.Models.Request;
using GeoFinder.Utility1.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFinder.Utility1.Services.Interface
{
    public interface IGeoFinderService
    {
        Task<BaseResponse> SignUp(SignUpViewModel signUpViewModel);
        Task<List<GetCountries>> GetCountries();
        Task<List<States>> GetState(string countryID);
        Task<SignInResponse> SignIn(Login login);
    }
}
 