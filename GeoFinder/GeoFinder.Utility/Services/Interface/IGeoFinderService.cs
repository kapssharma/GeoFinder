using GeoFinder.Utility.Models.Request;
using GeoFinder.Utility.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFinder.Utility.Services.Interface
{
    public interface IGeoFinderService
    {
        Task<BaseResponse> SignUp(SignUpViewModel signUpViewModel);
        Task<List<GetCountries>> GetCountries();
        Task<List<States>> GetState(string countryID);
        Task<SignInResponse> SignIn(Login login);
        Task<SearchResponse> Search(string? Search, string? Format);
        Task<StatusResponse> Status();
        Task<string> LookUp(string osm_id);


    }
}
