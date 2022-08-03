using System.ComponentModel.DataAnnotations;

namespace GeoFinder.API.Model
{
    public class Logs
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string LineNumber { get; set; }

        public string MethodName { get; set; }

        public string Message { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Action { get; set; }
    }
}
