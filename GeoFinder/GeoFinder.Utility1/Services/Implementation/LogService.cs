
using GeoFinder.Model;
using GeoFinder.Utility.Models.Request;
using GeoFinder.Utility.Models.Response;
using GeoFinder.Utility.Repository.Implementation;
using GeoFinder.Utility.Repository.Interface;
using GeoFinder.Utility.Services.Interface;
using System.Web;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using UAParser;
using System.Security.Cryptography;

namespace GeoFinder.Utility.Services.Implementation
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }
        public async Task AddSearchLog(string? request, string? response, string? endPoint, string? search, string? format, string? browserType, string? ipAddress)
        {
            try
            {
                SearchLog searchedLog = new SearchLog();
                searchedLog.Request = request;
                searchedLog.Response = response;
                searchedLog.EndPoint = endPoint;
                searchedLog.Search = search;
                searchedLog.Format = format;
                searchedLog.BrowserType = browserType;
                searchedLog.IPAddress = ipAddress;
                searchedLog.CreatedBy = "1";
                searchedLog.CreatedDate = DateTime.UtcNow;
                searchedLog.SearchType = "search";
                await _logRepository.AddLogSearch(searchedLog);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task AddReverseLog(string? request, string? response, string? endPoint, string? format, string? latitude, string? longitude, string? browserType, string? ipAddress)
        {
            try
            {
                SearchLog searchedLog = new SearchLog();
                searchedLog.Request = request;
                searchedLog.Response = response;
                searchedLog.EndPoint = endPoint;
                searchedLog.Format = format;
                searchedLog.Latitude = latitude;
                searchedLog.Longitude = longitude;
                searchedLog.BrowserType = browserType;
                searchedLog.IPAddress = ipAddress;
                searchedLog.CreatedBy = "1";
                searchedLog.CreatedDate = DateTime.UtcNow;
                searchedLog.SearchType = "reverse";
                await _logRepository.AddLogSearch(searchedLog);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        public async Task AddDetailLog(string? request, string? response, string? endPoint, string? osm_ID, string? browserType, string? ipAddress, string? searchType)
        {
            try
            {
                SearchLog searchedLog = new SearchLog();
                searchedLog.Request = request;
                searchedLog.Response = response;
                searchedLog.EndPoint = endPoint;
                if (osm_ID.All(Char.IsNumber))
                {
                    searchedLog.Place_Id = osm_ID;
                }
                else
                {
                    searchedLog.Osm_Type = osm_ID.Substring(0, 1);
                    searchedLog.Osm_Id = osm_ID.Remove(0, 1);
                }
                searchedLog.BrowserType = browserType;
                searchedLog.IPAddress = ipAddress;
                searchedLog.CreatedBy = "1";
                searchedLog.CreatedDate = DateTime.UtcNow;
                searchedLog.SearchType = searchType;
                await _logRepository.AddLogSearch(searchedLog);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}