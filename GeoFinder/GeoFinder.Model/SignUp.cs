using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFinder.Model
{
    public class SignUp
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; } 
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your EmailAdress")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string Password { get; set; }
       
    }
}
