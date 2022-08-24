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

        public async Task<List<GetCountries>> getCountries()
        {
            List<GetCountries> countries = new List<GetCountries>();
            try
            {
                countries = _db.Countries.Select(x => new GetCountries()
                {
                    CountryID = x.Id,
                    CountryName = x.Name
                }).ToList();
                return countries;
            }
            catch (Exception ex)
            {
                return countries;
            }
        }

        public async Task<List<States>> GetState(string countryID)
        {
            List<States> getStates = new List<States>();
            try
            {
                getStates = _db.States.Where(x => x.CountryId.ToString() == countryID & x.IsActive == true).Select(x => new States()
                {
                    StateId = (x.Id).ToString(),
                    Name = x.Name
                }).ToList();
                return getStates;
            }
            catch (Exception ex)
            {
                return getStates;
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

                UserToken newToken = new UserToken();
                newToken.UserId = newUsers.Id;
                newToken.CreatedOn = DateTime.Now;
                newToken.IsActive = true;
                newToken.TokenTypeID = _db.TokenType.Where(x => x.Token_Description == "Demo" && x.IsActive == true).Select(x => x.TokenTypeID).FirstOrDefault();

                _db.UserTokens.Add(newToken);
                _db.SaveChanges();
                signUpResponse.Success = true;
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
