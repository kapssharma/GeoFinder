using GeoFinder.API;
using GeoFinder.Model;
using GeoFinder.Model.Response;
using GeoFinder.Utility.Services.Interface;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;
using RestSharp;
using UAParser;

namespace GeoFinder.Utility.Services.Implementation;

public class SearchService : ISearchService
{
    private readonly HashSet<string> stopWords = new HashSet<string> { "the", "in", "near", "me", "and", "or", "for" };
    private readonly IConfiguration _configuration;
    public SearchService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<List<PlaceInfo>> SearchAsync(string? searchString)
    {
        try
        {
            if (string.IsNullOrEmpty(searchString))
                throw new Exception("input parameters are not correct for search");

            var currentLat = Convert.ToDouble("30.709330");
            var currentLon = Convert.ToDouble("76.689280");

            string apiEndPoint = this._configuration.GetSection("AppSettings")["NominatimAPIEndPoint"];
            string reverseURL = string.Format(apiEndPoint + "reverse?format=json&lat={0}&lon={1}", currentLat, currentLon);
            var restClient = new RestClient(reverseURL);
            var request = new RestRequest(reverseURL, Method.Get);
            var response = await restClient.ExecuteAsync(request);


            var queryVariations = await GenerateSubstrings(searchString);
            var tokens = searchString.Replace(",", " , ").Split(' ').Where(token => !stopWords.Contains(token.ToLower()) && (token != "")).ToList();


            //var queryVariations = GenerateLimitedQueryVariations(tokens);
            //queryVariations.Add(searchString);

            var allResults = new List<string>();
            foreach (var query in queryVariations)
            {
                var results = await PerformSearch(query);
                allResults.AddRange(results);
            }

            var rankedResults = RankResults(allResults, tokens);
            var result = SortPlacesByDistance(currentLat, currentLon, rankedResults);
            return result;
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }

    }
    public async Task<List<string>> GenerateSubstrings(string input)
    {
        var parts = input.Split(new[] { ", " }, StringSplitOptions.None);
        var result = new HashSet<string>();

        foreach (var part in parts)
        {
            var subwords = part.Split(' ');
            foreach (var word in subwords)
            {
                result.Add(word);
            }

            for (int i = 0; i < subwords.Length; i++)
            {
                for (int j = i + 1; j <= subwords.Length; j++)
                {
                    var combined = string.Join(" ", subwords.Skip(i).Take(j - i));
                    result.Add(combined);
                }
            }
        }

        return result.ToList();
    }
    private List<string> GenerateQueryVariations(List<string> tokens)
    {
        var variations = new List<string>();
        for (int i = 1; i <= tokens.Count; i++)
        {
            var permutations = GetPermutations(tokens, i);
            foreach (var perm in permutations)
            {
                variations.Add(string.Join(" ", perm));
            }
        }
        return variations;
    }
    private List<string> GenerateLimitedQueryVariations(List<string> tokens)
    {
        var groupedTokens = GroupTokens(tokens);
        var variations = new List<string>();

        foreach (var group in groupedTokens)
        {
            var permutations = GetPermutations(group, group.Count);
            foreach (var perm in permutations)
            {
                variations.Add(string.Join(" ", perm));
            }
        }

        return variations;
    }
    private List<List<string>> GroupTokens(List<string> tokens)
    {
        var groups = new List<List<string>>();
        var currentGroup = new List<string>();

        foreach (var token in tokens)
        {
            if (stopWords.Contains(token.ToLower()) || token == ",")
            {
                if (currentGroup.Count > 0)
                {
                    groups.Add(new List<string>(currentGroup));
                    currentGroup.Clear();
                }
            }
            else
            {
                currentGroup.Add(token);
            }
        }

        if (currentGroup.Count > 0)
        {
            groups.Add(currentGroup);
        }

        return groups;
    }
    private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
    {
        if (length == 1) return list.Select(t => new T[] { t });
        return GetPermutations(list, length - 1)
            .SelectMany(t => list.Where(e => !t.Contains(e)),
                        (t1, t2) => t1.Concat(new T[] { t2 }));
    }

    private async Task<List<string>> PerformSearch(string query)
    {
        string apiEndPoint = this._configuration.GetSection("AppSettings")["NominatimAPIEndPoint"];
        var searchUrl = string.Format(apiEndPoint + "search?q={0}&format=json", query);
        var restClient = new RestClient(searchUrl);
        var request = new RestRequest(searchUrl, Method.Get);
        var response = await restClient.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
            return new List<string> { response?.Content };
        }
        else
        {
            throw new HttpRequestException(response.ErrorMessage);
        }
    }

    private List<PlaceInfo> RankResults(List<string> results, List<string> originalTokens)
    {
        List<PlaceInfo> allplaces = new List<PlaceInfo>();
        var rankedResults = new List<Tuple<PlaceInfo, int>>();
        foreach (var result in results)
        {
            List<PlaceInfo> places = JsonConvert.DeserializeObject<List<PlaceInfo>>(result);
            allplaces.AddRange(places);
        }
        var uniqueOSM_Ids = allplaces.Select(x => x.Osm_Id).Distinct().ToList();
        foreach (var osm_id in uniqueOSM_Ids)
        {
            var place = allplaces.Where(x => x.Osm_Id == osm_id).FirstOrDefault();
            int relevanceScore = originalTokens.Sum(token => CountOccurrences(place?.Display_Name, token));
            rankedResults.Add(new Tuple<PlaceInfo, int>(place, relevanceScore));
        }

        return rankedResults.OrderByDescending(r => r.Item2).Select(r => r.Item1).ToList();
    }

    private int CountOccurrences(string text, string token)
    {
        int count = 0;
        int index = 0;

        while ((index = text.IndexOf(token, index, StringComparison.OrdinalIgnoreCase)) != -1)
        {
            count++;
            index += token.Length;
        }

        return count;
    }
    public static double Haversine(double lat1, double lon1, double lat2, double lon2)
    {
        const double R = 6371;
        double lat1Rad = DegreesToRadians(lat1);
        double lon1Rad = DegreesToRadians(lon1);
        double lat2Rad = DegreesToRadians(lat2);
        double lon2Rad = DegreesToRadians(lon2);

        double dlat = lat2Rad - lat1Rad;
        double dlon = lon2Rad - lon1Rad;

        double a = Math.Sin(dlat / 2) * Math.Sin(dlat / 2) +
                   Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                   Math.Sin(dlon / 2) * Math.Sin(dlon / 2);
        double c = 2 * Math.Asin(Math.Sqrt(a));

        double distance = R * c;
        return distance;
    }

    public static double DegreesToRadians(double degrees)
    {
        return degrees * Math.PI / 180;
    }

    public static List<PlaceInfo> SortPlacesByDistance(double yourLat, double yourLon, List<PlaceInfo> places)
    {
        var distances = places.Select(place => new
        {
            Place = place,
            Distance = Haversine(yourLat, yourLon, Convert.ToDouble(place?.Lat), Convert.ToDouble(place?.Lon))
        })
        .OrderBy(x => x.Distance)
        .Select(x => x.Place)
        .ToList();

        return distances;
    }
   

}
