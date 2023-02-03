using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GeoFinder.Utility.Models.Request
{
    public class GetCountries
    {
        public Guid CountryID { get; set; }

        public string CountryName { get; set;}
    }
}
 