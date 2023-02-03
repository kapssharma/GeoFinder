using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFinder.Utility1.Models.Request
{
    public class Login
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public int LoginType { get; set; }
    }
}
 