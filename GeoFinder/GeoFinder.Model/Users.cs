using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFinder.Model
{
    public class Users
    {
        [Key]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Please enter your UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter your EmailAdress")]
        public string EmailAdress { get; set; }

        [Required(ErrorMessage = "Please enter your Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please select your Role")]
        [Display(Name = "Role")]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Roles Roles { get; set; }

        public String CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public String ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        [Display(Name = "Address")]
        public int AddressId { get; set; }

        [ForeignKey("AddressId")]
        public virtual Address Address { get; set;}

        public bool IsActive { get; set; }


    }
}
