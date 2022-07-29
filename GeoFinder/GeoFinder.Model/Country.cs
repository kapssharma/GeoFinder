using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFinder.Model
{
    public class Country
    {
        public Guid CountryId { get; set; }

        [Required]
        public String CountryName { get; set; }

        public String CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public String ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
