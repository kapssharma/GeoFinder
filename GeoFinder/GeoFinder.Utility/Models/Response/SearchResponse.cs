using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFinder.Utility.Models.Response
{
    public class SearchResponse : BaseResponse
    {
        public XMLSearchResponse XMLSearchResponse { get; set; }
        public List<JsonSearchResponse> JsonSearchResponse { get; set; }
    }
}
