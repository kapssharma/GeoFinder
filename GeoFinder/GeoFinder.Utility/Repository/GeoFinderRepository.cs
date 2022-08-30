using GeoFinder.Data;
using GeoFinder.Model;
using GeoFinder.Utility.Models.Request;
using GeoFinder.Utility.Models.Response;
using GeoFinder.Utility.Services.Interface;

using Microsoft.Extensions.Logging;

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
        private readonly ILogger<GeoFinderRepository> _logger;

        public GeoFinderRepository(ILogger<GeoFinderRepository> logger, ApplicationDbContext db)
        {
            _logger = logger;
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
                 _logger.LogError($"Error While Getting Countries Names  : { ex.Message}",
                         DateTime.UtcNow.ToLongTimeString());
                return countries;
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
                newUsers.CreatedOn = DateTime.Now;
                newUsers.IsActive = true;
                return signUpResponse;
            }
            catch (Exception ex)
            {
                signUpResponse.Success = false;
                signUpResponse.Message = ex.Message;
                _logger.LogError($"Error while Saving Data : { ex.Message}",
                        DateTime.UtcNow.ToLongTimeString());
                return signUpResponse;
            }
        }

        public async Task<string> CheckAndReturnSeachResult(string searchText, string format)
        {
            var check = _db.Format.Where(x => x.Name == format).FirstOrDefault();
            return _db.SearchHistory.Where(x => x.SearchName == searchText && x.SearchFormat == _db.Format.Where(x => x.Name == format).FirstOrDefault()) ?.FirstOrDefault()?.SearchResult ?? string.Empty; 
        }

        public async Task<bool> SaveSearchHistory(string contentResponse, string search, string format)
        {
            try
            {
                var result = _db.SearchHistory.Add(new SearchHistory()
                {
                    SearchName = search,
                    SearchByuser = _db.Users.FirstOrDefault(),
                    SearchResult = contentResponse,
                    SearchOn = DateTime.Now,
                    SearchFormat = _db.Format.Where(x => x.Name == format).FirstOrDefault()
                });
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Search() : {ex.Message}",
                         DateTime.UtcNow.ToLongTimeString());
                return false;
            }
        }

        
    }
}
