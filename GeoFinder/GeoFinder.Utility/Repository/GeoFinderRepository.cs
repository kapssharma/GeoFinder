using GeoFinder.Data;
using GeoFinder.Model;
using GeoFinder.Utility.Models.Request;
using GeoFinder.Utility.Models.Response;
using GeoFinder.Utility.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFinder.Utility.Repository
{
    public class GeoFinderRepository : IGeoFinderRepository
    {
        private readonly ApplicationDbContext _db;

        public GeoFinderRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task<BaseResponse> getCountries()
        {
            BaseResponse signUpResponse = new BaseResponse();
            try
            {
                var countries = _db.Countries;
                List<string> countries1 = _db.Countries;
                return countries1;
            }
            catch (Exception ex)
            {
                signUpResponse.Success = false;
                signUpResponse.Message = ex.Message;
                return signUpResponse;
            }
        }

        public async Task<SignUpResponse> SignUp(SignUpViewModel signUpViewModel)
        {
            SignUpResponse signUpResponse = new SignUpResponse();
            try
            {
                Users newUsers = new Users();
                newUsers.Name = signUpViewModel.Name;
                newUsers.EmailAddress = signUpViewModel.Email;
                //newUsers.Password = signUpViewModel.Password;
                newUsers.CreatedOn = DateTime.Now;
                newUsers.IsActive = true;

                _db.Users.Add(newUsers);

                Token newToken = new Token();
                newToken.UserId = newUsers.Id;
                newToken.CreatedBy = newUsers.Id;
                newToken.CreatedOn = DateTime.Now;
                newToken.IsActive = true;
                newToken.TokenName = Guid.NewGuid().ToString().Replace("-", "");

                _db.Tokens.Add(newToken);
                _db.SaveChanges();
                signUpResponse.Success = true;
                signUpResponse.Token = newToken.TokenName;
                signUpResponse.Message = "SignUp Successfully";
                return signUpResponse;
            }
            catch (Exception ex)
            {
                signUpResponse.Success = false;
                signUpResponse.Message = ex.Message;
                return signUpResponse;
            }
        }
    }
}
