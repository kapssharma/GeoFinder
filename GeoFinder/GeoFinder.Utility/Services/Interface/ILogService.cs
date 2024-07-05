using GeoFinder.Model;
using GeoFinder.Utility.Models.Request;
using GeoFinder.Utility.Models.Response;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFinder.Utility.Services.Interface
{
    public interface ILogService
    {
        Task AddSearchLog(string? request, string? response, string? endPoint, string? search, string? format, string? browserType, string? ipAddress);
        Task AddReverseLog(string? request, string? response, string? endPoint, string? format, string? latitude, string? longitude, string? browserType, string? ipAddress);
        Task AddDetailLog(string? request, string? response, string? endPoint, string? osm_ID, string? browserType, string? ipAddress, string? searchType);
    }
}
