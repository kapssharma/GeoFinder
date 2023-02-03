using GeoFinder.Data;
using GeoFinder.Model;
using GeoFinder.Utility1.Models.Request;
using GeoFinder.Utility1.Models.Response;
using GeoFinder.Utility1.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using Microsoft.Extensions.Configuration;

namespace GeoFinder.Utility1.Repository
{
    public class GeoFinderRepository : IGeoFinderRepository
    {
        private readonly ApplicationDbContext _db;
        private IConfiguration _configuration;
        private IEmailService _emailService;
        public GeoFinderRepository(ApplicationDbContext db, IConfiguration configuration, IEmailService emailService) 
        {
            _db = db;
            _configuration = configuration;
            _emailService = emailService;
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

        public async Task<BaseResponse> SignUp(SignUpViewModel signUpViewModel)
        {
            BaseResponse signUpResponse = new BaseResponse();
            try
            {
                Users newUsers = new Users();
                newUsers.Name = signUpViewModel.Name;
                newUsers.EmailAddress = signUpViewModel.Email;
                newUsers.Password = (signUpViewModel.Password);
                newUsers.CreatedOn = DateTime.Now;
                newUsers.IsActive = true;
                //newUsers.IsVerified = false;

                _db.Users.Add(newUsers);
                Address newAddress = new Address();
                newAddress.CountryId = Guid.Parse(signUpViewModel.Country);
                //newAddress.StateId = Convert.ToInt32(signUpViewModel.State);
                newAddress.City = signUpViewModel.City;
                newAddress.PostalCode = signUpViewModel.PIN_Code;
                newAddress.IsActive = true;
                newAddress.CreatedOn = DateTime.Now;
                newAddress.CreatedBy = newUsers.Id;
                _db.Add(newAddress);

                newUsers.AddressId = newAddress.Id;

                UserToken newToken = new UserToken();
                newToken.UserId = newUsers.Id;
                newToken.CreatedOn = DateTime.Now;
                newToken.IsActive = true;
                newToken.TokenTypeID = _db.TokenTypes.Where(x => x.Token_Description == "Demo" && x.IsActive == true).Select(x => x.TokenTypeID).FirstOrDefault();

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
        public async Task<SignInResponse> SignIn(Login signInModel)
        {
            SignInResponse signUpResponse = new SignInResponse();
            try
            {
                string receiverEmail = signInModel.EmailAddress;
                if (signInModel.LoginType == 1)
                {
                    Guid userID = _db.Users.Where(x => x.EmailAddress == receiverEmail && x.IsActive == true).Select(x => x.Id).FirstOrDefault();
                    Guid token = _db.UserTokens.Where(x => x.UserId == userID && x.IsActive == true).Select(x => x.TokenID).FirstOrDefault();
                    string body = "<a href='www.GeoFindrDashboard.com?Token=" + token + "'>Click me</a>";
                    string subject = "GeoFinder Login Request";
                    await _emailService.SendEmailAsync(_configuration, receiverEmail, body, subject);
                    signUpResponse.Success = true;
                    signUpResponse.Message = "Mail Sent Successfully";
                }
                else
                {
                    string password = signInModel.Password;
                    string emailAddr = _db.Users.Where(x => x.EmailAddress == receiverEmail && x.IsActive == true).Select(x => x.EmailAddress).FirstOrDefault();
                    string pass = _db.Users.Where(x => x.Password == password && x.IsActive == true).Select(x => x.Password).FirstOrDefault();
                    if (!string.IsNullOrEmpty(emailAddr))
                    {
                        if (!string.IsNullOrEmpty(pass))
                        {
                            signUpResponse.Success = true;
                            signUpResponse.Message = "Login Successfully";
                        }
                        else
                        {
                            signUpResponse.Success = false;
                            signUpResponse.Message = "Password is Incorrect";
                        }
                    }
                    else
                    {
                        signUpResponse.Success = false;
                        signUpResponse.Message = "Email is Incorrect";
                    }
                }
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
