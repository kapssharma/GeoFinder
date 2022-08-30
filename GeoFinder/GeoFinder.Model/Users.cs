﻿using System;
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

        
        public string? Password { get; set; }

        
        [ForeignKey("RoleId")]
        public Guid? RoleId { get; set; }

        [Required(ErrorMessage = "Please select your Role")]
        [Display(Name = "Role")]
        public virtual Roles Roles { get; set; }

        public Guid? CreatedBy { get; set; }
        [ForeignKey("Id")]
        public Users? Createdbyuser { get; set; }

        public DateTime CreatedOn { get; set; }
        [ForeignKey("ModifiedById")]
        public Guid? ModifiedBy { get; set; }
        
        public Users? Modifiedbyuser { get; set; }

        public DateTime? ModifiedOn { get; set; }
        [ForeignKey("AddressId")]
        public Guid? AddressId { get; set; }

        [Display(Name = "Address")]
        public virtual Address? Address { get; set;}

        public bool IsActive { get; set; }

        public bool IsVerified { get; set; }


    }
}
