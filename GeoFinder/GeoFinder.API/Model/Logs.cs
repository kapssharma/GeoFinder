using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoFinder.API.Model
{
    public class Logs
    {
        [Key]
        public Guid Id { get; set; }
        public string Request { get; set; }

        public string Response { get; set; }

        public string EndPoint { get; set; }

        public string BrowserType { get; set; }

        public string IPAddress { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public int User_ID { get; set; }

        [ForeignKey("Id")]
        public int StatusID { get; set; }
     
    }
    
}
    

