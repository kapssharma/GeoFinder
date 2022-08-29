using GeoFinder.Utility.Enum;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFinder.Utility.Models.Request
{
    public class SearchViewRequest
    {
        [EnumDataType(typeof(string))]
        public Format Format { get; set; }
    }
}
