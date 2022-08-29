using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFinder.Utility.Models.Response
{

    public class StatusResponse : BaseResponse
    {
        public StatusResponseData ResponseData { get; set; }
        public class StatusResponseData
        {
            public int? status { get; set; }
            public string message { get; set; }
            public DateTime data_updated { get; set; }
            public string software_version { get; set; }
            public string database_version { get; set; }
        }

    }



}
