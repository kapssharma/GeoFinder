using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFinder.Utility.Models.Request
{
    public class GetCountries
    {
        public Guid SelectedCoutry { get; set; }

        public List<SelectListItem> AvailableCountries { get; set; }
    }
}
