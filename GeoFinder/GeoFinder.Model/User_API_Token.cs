using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFinder.Model
{
    public class User_API_Token
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("Id")]
        public Users User_Id { get; set; }
        public DateTime Expiry { get; set; }
        public Guid CreatedBy { get; set; }
        [ForeignKey("Id")]
        public Users Createdbyuser { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid ModifiedBy { get; set; }
        [ForeignKey("Id")]
        public Users Modifiedbyuser { get; set; }

        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}

