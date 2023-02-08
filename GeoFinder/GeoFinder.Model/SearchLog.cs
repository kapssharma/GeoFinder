using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFinder.Model
{
    public class SearchLog
    {

        [Key]
        public Guid Id { get; set; }
        public string? Request { get; set; }
        public string? Response { get; set; }
        public string? Search { get; set; }
        public string? Format { get; set; }
        public string? Osm_Id { get; set; }
        public string? Osm_Type { get; set; }
        public string? Place_Id { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
        public string? SearchType { get; set; }
        public string? EndPoint { get; set; }

        public string? BrowserType { get; set; }

        public string? IPAddress { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public int? User_ID { get; set; }


        //[ForeignKey("Id")]
        //public int StatusID { get; set; }



    }
}
