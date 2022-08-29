using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFinder.Model
{
    public class SearchHistory
    {
        [Key]
        public int SearchHistoryId { get; set; }
        public string SearchName  { get; set; }
        // public string SearchBy { get; set; }
        [ForeignKey("UserId")]
        public Users? SearchByuser { get; set; }

        public DateTime SearchOn { get; set; }

        public string SearchResult { get; set; }
        [ForeignKey("FormatId")]
        public Format? SearchFormat { get; set; }
    }
}
