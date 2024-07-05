using GeoFinder.Utility.Models.Request;
using GeoFinder.Utility.Models.Response;
using GeoFinder.Utility.Repository.Interface;
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
            try
            {
                var response = await _geoFinderRepository.SignUp(signUpViewModel);
                return response;
            }
            catch (Exception ex)
            {

                BaseResponse signUpResponse = new BaseResponse();
                return signUpResponse;
            }
        }
        public async Task<SignInResponse> SignIn(Login login)
        {
            var response = await _geoFinderRepository.SignIn(login);
            return response;
        }
    }
}
