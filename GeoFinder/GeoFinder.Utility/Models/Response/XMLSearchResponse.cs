using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GeoFinder.Utility.Models.Response
{
	public class XMLSearchResponse
	{
		public Searchresults SearchResults { get; set; }
		[XmlRoot(ElementName = "place")]
		public class Place
		{
			[XmlAttribute(AttributeName = "place_id")]
			public string Place_id { get; set; }
			[XmlAttribute(AttributeName = "osm_type")]
			public string Osm_type { get; set; }
			[XmlAttribute(AttributeName = "osm_id")]
			public string Osm_id { get; set; }
			[XmlAttribute(AttributeName = "place_rank")]
			public string Place_rank { get; set; }
			[XmlAttribute(AttributeName = "address_rank")]
			public string Address_rank { get; set; }
			[XmlAttribute(AttributeName = "boundingbox")]
			public string Boundingbox { get; set; }
			[XmlAttribute(AttributeName = "lat")]
			public string Lat { get; set; }
			[XmlAttribute(AttributeName = "lon")]
			public string Lon { get; set; }
			[XmlAttribute(AttributeName = "display_name")]
			public string Display_name { get; set; }
			[XmlAttribute(AttributeName = "class")]
			public string Class { get; set; }
			[XmlAttribute(AttributeName = "type")]
			public string Type { get; set; }
			[XmlAttribute(AttributeName = "importance")]
			public string Importance { get; set; }
			[XmlAttribute(AttributeName = "icon")]
			public string Icon { get; set; }
		}

		[XmlRoot(ElementName = "searchresults")]
		public class Searchresults
		{
			[XmlElement(ElementName = "place")]
			public List<Place> Place { get; set; }
			[XmlAttribute(AttributeName = "timestamp")]
			public string Timestamp { get; set; }
			[XmlAttribute(AttributeName = "attribution")]
			public string Attribution { get; set; }
			[XmlAttribute(AttributeName = "querystring")]
			public string Querystring { get; set; }
			[XmlAttribute(AttributeName = "exclude_place_ids")]
			public string Exclude_place_ids { get; set; }
			[XmlAttribute(AttributeName = "more_url")]
			public string More_url { get; set; }
		}

	}
}

