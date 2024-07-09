using GeoFinder.Model.Response;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFinder.Utility.Services.Interface;

public interface ISearchService
{
    Task<List<PlaceInfo>> SearchAsync(string? searchString);
}
