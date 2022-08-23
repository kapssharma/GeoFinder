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
        Task<SignUpResponse> SignUp(SignUpViewModel signUpViewModel);
        Task<BaseResponse> getCountries();
    }
}
