using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFinder.Model
{
    public class Token
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string TokenName { get; set; }

        [ForeignKey("UserId")]
        public Users? User_Id { get; set; }
        public DateTime? Expiry { get; set; }
        public Guid CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public Users? CreatedByUser { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid? ModifiedBy { get; set; }
        [ForeignKey("ModifiedBy")]
        public Users? ModifiedByuser { get; set; }

        public DateTime? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
