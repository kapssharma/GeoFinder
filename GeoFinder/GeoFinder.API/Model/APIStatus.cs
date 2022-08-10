using System.ComponentModel.DataAnnotations;

namespace GeoFinder.API.Model
{
    public class APIStatus
    {
        [Key]
        public Guid ID { get; set; }

        public string Status { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifyOn { get; set; }

        public string ModifyBy { get; set; }

        public bool IsActive { get; set; }

        public string values { get; set; }
    }
}
