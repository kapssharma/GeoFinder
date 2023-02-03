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
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Please enter your UserName")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your EmailAdress")] 
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Please enter your Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please select your Role")]
        [Display(Name = "Role")]
        public Guid RoleId { get; set; }

        [ForeignKey("Id")]
        public virtual Roles Roles { get; set; }

        public Guid CreatedBy { get; set; }
        [ForeignKey("Id")]
        public Users Createdbyuser { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid ModifiedBy { get; set; }
        [ForeignKey("Id")]
        public Users Modifiedbyuser { get; set; }

        public DateTime ModifiedOn { get; set; }

        [Display(Name = "Address")]
        public Guid AddressId { get; set; }

        [ForeignKey("Id")]
        public virtual Address Address { get; set;}

        public bool IsActive { get; set; }


    }
}
