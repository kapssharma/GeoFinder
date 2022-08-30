﻿using GeoFinder.Utility.Models.Request;
using GeoFinder.Utility.Models.Response;
using GeoFinder.Utility.Repository;
using GeoFinder.Utility.Services.Interface;

using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;

using RestSharp;
using System.Xml.Serialization;
using GeoFinder.Utility.Constants;

using static GeoFinder.Utility.Models.Response.StatusResponse;
using static GeoFinder.Utility.Models.Response.XMLSearchResponse;
using Microsoft.Extensions.Logging;

namespace GeoFinder.Utility.Services.Implementation
{
    public class GeoFinderService : IGeoFinderService
    {
        private readonly ILogger<GeoFinderService> _logger;
        private readonly IGeoFinderRepository _geoFinderRepository;
        private readonly IConfiguration configuration;

        public GeoFinderService(ILogger<GeoFinderService> logger, IConfiguration _configuration, IGeoFinderRepository geoFinderRepository)
        {
            _logger = logger;
            configuration = _configuration;
            _geoFinderRepository = geoFinderRepository;

        }
        /// <summary>
        /// Get all Countries
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Get Searched response  on the basis of Format
        /// </summary>
        /// <param name="search"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public async Task<SearchResponse> Search(string? search, string format)
        {
            SearchResponse searchResponse = new SearchResponse();
            try
            {
                if (string.IsNullOrEmpty(search) || string.IsNullOrEmpty(format))
                {
                    searchResponse.Success = false;
                    searchResponse.Message = "Please Enter Search Data and Format";
                    return searchResponse;
                }

                var contentResponse = string.Empty;
                var searchResult = await _geoFinderRepository.CheckAndReturnSeachResult(search, format);

                if (!string.IsNullOrEmpty(searchResult))
                {
                    contentResponse = searchResult;
                }
                else
                {
                    string apiEndPoint = this.configuration.GetSection("AppSettings")["NominatimAPIEndPoint"];
                    var SearchUrl = string.Format(apiEndPoint + "search?q={0}&format={1}", search, format);
                    var restClient = new RestClient(SearchUrl);
                    var request = new RestRequest(SearchUrl, Method.Get);
                    var response = await restClient.ExecuteAsync(request);
                    if (response.IsSuccessful)
                    {
                        contentResponse = response.Content;
                    }
                    else
                    {
                        _logger.LogError($"Error Recieveing Response from API : {response.ErrorMessage}",
                         DateTime.UtcNow.ToLongTimeString());
                        searchResponse.Success = false;
                        searchResponse.Message = response.ErrorMessage;
                        return searchResponse;
                    }
                }

                if (string.Equals(format, GeoFinderConstants.XmlFormat, StringComparison.OrdinalIgnoreCase))
                {
                    var serializer = new XmlSerializer(typeof(Searchresults));
                    XMLSearchResponse xmlSearchResponse = new XMLSearchResponse();
                    Searchresults searchresults = new Searchresults();
                    using (var reader = new StringReader(contentResponse))
                    {
                        searchresults = (Searchresults)serializer.Deserialize(reader);
                        xmlSearchResponse.SearchResults = searchresults;
                    }

                    searchResponse.Success = true;
                    searchResponse.Message = "Searched Successfully";
                    searchResponse.XMLSearchResponse = xmlSearchResponse;

                    //Method for Save Response
                    if (string.IsNullOrEmpty(searchResult))
                        await _geoFinderRepository.SaveSearchHistory(contentResponse, search, format);
                    return searchResponse;
                }

                if (string.Equals(format, GeoFinderConstants.JsonFormat, StringComparison.OrdinalIgnoreCase))
                {
                    List<JsonSearchResponse> jsonSearchResponse = new List<JsonSearchResponse>();
                    jsonSearchResponse = JsonConvert.DeserializeObject<List<JsonSearchResponse>>(contentResponse);
                    searchResponse.Success = true;
                    searchResponse.Message = "Searched Successfully";
                    searchResponse.JsonSearchResponse = jsonSearchResponse;
                    if (string.IsNullOrEmpty(searchResult))
                        await _geoFinderRepository.SaveSearchHistory(contentResponse, search, format);
                    return searchResponse;
                }
                return searchResponse;
            }
            catch (Exception ex)
            {
                searchResponse.Success = false;
                searchResponse.Message = ex.Message;
                _logger.LogError($"Error in Searching:{search} Error : {ex.Message}",
                         DateTime.UtcNow.ToLongTimeString());
                return searchResponse;
            }
        }
        public async Task<StatusResponse> Status()
        {
            StatusResponse statusResponse = new StatusResponse();
            try
            {
                var contentResponse = "";
                string apiEndPoint = this.configuration.GetSection("AppSettings")["NominatimAPIEndPoint"];
                var statusUrl = string.Format(apiEndPoint + "status.php?format=json");
                var restClient = new RestClient(statusUrl);
                var request = new RestRequest(statusUrl, Method.Get);
                var response = await restClient.ExecuteAsync(request);
                if (response.IsSuccessful)
                {
                    contentResponse = response.Content;
                }
                else
                {
                    throw new HttpRequestException(response.ErrorMessage);
                }

                StatusResponseData statusResponseData = new StatusResponseData();
                statusResponseData = JsonConvert.DeserializeObject<StatusResponseData>(contentResponse);
                statusResponse.ResponseData = statusResponseData;
                statusResponse.Success = true;
                statusResponse.Message = "Status";
                return statusResponse;
            }
            catch (Exception ex)
            {
                statusResponse.Success = false;
                statusResponse.Message = "failed";
                _logger.LogError($"Error in getting Status: {ex.Message}",
                         DateTime.UtcNow.ToLongTimeString());
                return statusResponse;
            }
        }


        public async Task<string> LookUp(string? osm_id)
        {
            var contentResponse = "";
            string apiEndPoint = this.configuration.GetSection("AppSettings")["GetNominatimBaseURL"];
            string lookupURL = string.Format(apiEndPoint + "lookup?osm_ids={0}", osm_id);
            var restClient = new RestClient(lookupURL);
            var request = new RestRequest(lookupURL, Method.Get);
            var response = await restClient.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                contentResponse = response.Content;
            }
            else
            {
                throw new HttpRequestException(response.ErrorMessage);

            }
            return contentResponse;
        }
    }
}
