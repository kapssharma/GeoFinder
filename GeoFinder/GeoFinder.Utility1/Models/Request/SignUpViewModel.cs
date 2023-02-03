using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFinder.Utility1.Models.Request
{
    public class SignUpViewModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PIN_Code { get; set; }
        public string Password { get; set; }

    }
}
 