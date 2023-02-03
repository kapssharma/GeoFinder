using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFinder.Model
{
    public class TokenType
    {
        [Key]
        public Guid TokenTypeID { get; set; }
        public string Token_Description { get; set; }
        public bool IsActive { get; set; }
    }
} 
