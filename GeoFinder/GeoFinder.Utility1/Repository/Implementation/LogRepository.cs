using GeoFinder.Data;
using GeoFinder.Model;
using GeoFinder.Utility.Models.Request;
using GeoFinder.Utility.Models.Response;
using GeoFinder.Utility.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using Microsoft.Extensions.Configuration;
using GeoFinder.Utility.Repository.Interface;
using System.Net.Http;
using UAParser;


namespace GeoFinder.Utility.Repository.Implementation
{
    public class LogRepository : ILogRepository
    {
        private readonly ApplicationDbContext _db;
        private IConfiguration _configuration;
        public LogRepository(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }
        public async Task AddLogSearch(SearchLog searchLog)
        {
            _db.SearchLog.Add(searchLog);
            _db.SaveChanges();

        }
    }
}