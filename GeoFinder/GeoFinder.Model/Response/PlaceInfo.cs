using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFinder.Model.Response
{
    public class PlaceInfo
    {
        public long Place_Id { get; set; }
        public string Licence { get; set; }
        public string Osm_Type { get; set; }
        public long Osm_Id { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string Class { get; set; }
        public string Type { get; set; }
        public int Place_Rank { get; set; }
        public double Importance { get; set; }
        public string Addresstype { get; set; }
        public string Name { get; set; }
        public string Display_Name { get; set; }
        public string[] Boundingbox { get; set; }
    }
}
