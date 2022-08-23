using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFinder.Model
{
    public class UserToken
    {
        [Key]
        public Guid TokenID { get; set; }
        public Guid? UserId { get; set; }     
        public virtual Users User { get; set; }
        public Guid TokenTypeID { get; set; }
        public virtual TokenType TokenType { get; set; }
        public Guid? CreatedByUserID { get; set; }      
        public virtual Users CreatedByUser { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? ModifyByUserID { get; set; }        
        public virtual Users ModifyByUser { get; set; }
        public DateTime? ModifyOn { get; set; }
        public bool IsActive { get; set; }
    }
}
