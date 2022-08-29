using GeoFinder.Model;
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
        Task<List<GetCountries>> getCountries();
        Task<string> CheckAndReturnSeachResult(string searchText,string format);
        Task<bool> SaveSearchHistory(string contentResponse , string search, string format );
      
       
    }
}
