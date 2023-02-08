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

        public  int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public bool IsActive { get; set; }

    }
}
