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

        public virtual Guid Country { get; set; }

        public Guid CreatedBy { get; set; }
        public virtual Guid Createdbyuser { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid ModifiedBy { get; set; }
        public virtual Guid Modifiedbyuser { get; set; }

        public DateTime ModifiedOn { get; set; }

        public bool IsActive { get; set; }

    }
}
