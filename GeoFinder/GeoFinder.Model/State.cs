using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFinder.Model
{
    public class State
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String Name { get; set; }

        public  Guid CountryId { get; set; }

        [ForeignKey("Id")]
        public  Country Country { get; set; }

        public Guid CreatedBy { get; set; }
        [ForeignKey("Id")]
        public Users User { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid ModifiedBy { get; set; }
        [ForeignKey("Id")]
        public Users Users { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
