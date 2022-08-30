using GeoFinder.Utility.Models.Request;
using GeoFinder.Utility.Models.Response;
using GeoFinder.Utility.Repository;
using GeoFinder.Utility.Services.Interface;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFinder.Utility.Services.Implementation
{
    public class GeoFinderService : IGeoFinderService
    {
        private readonly IGeoFinderRepository _geoFinderRepository;

        public GeoFinderService(IGeoFinderRepository geoFinderRepository)
        {
            _geoFinderRepository = geoFinderRepository;
        }

        public async Task<List<GetCountries>> GetCountries()
        {
            var result = await _geoFinderRepository.getCountries();
            return result;
        }
        public async Task<List<States>> GetState(string stateID)
        {
            var result = await _geoFinderRepository.GetState(stateID);
            return result;
        }
        public async Task<BaseResponse> SignUp(SignUpViewModel signUpViewModel)
        {
            var response = await _geoFinderRepository.SignUp(signUpViewModel);
            return response;
        }
        public async Task<SignInResponse> SignIn(Login login)
        {
            var response = await _geoFinderRepository.SignIn(login);
            return response;
        }
    }
}
