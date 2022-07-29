using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFinder.Model
{
    public class Roles
    {
        [Key]
        public Guid RoleId  { get; set; }

        [Required]
        public string RoleName { get; set; }

        public String CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public String ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
