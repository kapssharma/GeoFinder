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
        public Guid AddressId { get; set; }

        [Required(ErrorMessage = "Please enter your City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please Select your State")]
        [Display(Name = "Country")]
        public int StateId { get; set; }

        [ForeignKey("StateId")]
        public virtual State State { get; set; }

        [Required(ErrorMessage = "Please Select your Country")]
        [Display(Name ="Country")]
        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }

        [Required(ErrorMessage = "Please enter your PostalCode")]
        public string PostalCode { get; set; }


        public String CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public String ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

    }
}
