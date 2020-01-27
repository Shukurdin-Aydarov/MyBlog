using System.Linq;
using System.Collections.Generic;
using MongoDB.Driver.GeoJsonObjectModel;

namespace Location.Api.Models
{
    public class Polygon
    {
        public static readonly string GeoType = "Polygon";

        public Polygon() { }

        public Polygon(IEnumerable<GeoJson2DGeographicCoordinates> coordinates)
        {
           this.coordinates = coordinates.Select(x => new[] { x.Longitude, x.Latitude })
                                         .ToArray();
        }

        public string type { get; private set; } = GeoType;
        
        public double[][] coordinates { get; private set; }
    }
}
