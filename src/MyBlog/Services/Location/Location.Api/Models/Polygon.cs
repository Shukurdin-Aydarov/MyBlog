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
           Coordinates = coordinates.Select(x => new[] { x.Longitude, x.Latitude })
                                    .ToArray();
        }

        public string Type { get; } = GeoType;
        
        public double[][] Coordinates { get; }
    }
}
