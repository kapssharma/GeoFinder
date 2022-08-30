using GeoFinder.Utility.Models.Request;
using GeoFinder.Utility.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFinder.Utility.Repository
{
    public interface IGeoFinderRepository
    {
        Task<BaseResponse> SignUp(SignUpViewModel signUpViewModel);
        Task<List<GetCountries>> getCountries();
        Task<List<States>> GetState(string countryID);
        Task<SignInResponse> SignIn(Login login);
        Task<string> CheckAndReturnSeachResult(string searchText, string format);
        Task<bool> SaveSearchHistory (string contentResponse,string searchText, string format);
    }
}
