using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFinder.Model
{
    public class Address
    {
        [Key]
        public  Guid Id { get; set; }

        [Required(ErrorMessage = "Please enter your City")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Please select your State")]
        [Display(Name = "State")]
        public int StateId { get; set; }
        public virtual State State { get; set; }

        [Required(ErrorMessage = "Please select your Country")]
        [Display(Name = "Country")]
        public Guid CountryId { get; set; }
        public virtual Country? Country { get; set; }

        [Required(ErrorMessage = "Please enter your PostalCode")]
        public string? PostalCode { get; set; }

        public Guid CreatedBy { get; set; }
        public virtual Users Createdbyuser { get; set; }

        public DateTime CreatedOn { get; set; }

        //public Guid ModifiedBy { get; set; }
        //[ForeignKey("ModifiedById")]
        //public Users? Modifiedbyuser { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsActive { get; set; }

    }
}
