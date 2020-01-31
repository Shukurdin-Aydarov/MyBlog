using Newtonsoft.Json;

namespace MyBlog.Location.Api.Models
{
    public class Point
    {
        public static readonly string GeoType = "Point";

        public Point() { }

        [JsonConstructor]
        public Point(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;

            coordinates = new[] { longitude, latitude };
        }

        public double Longitude { get; }

        public double Latitude { get; }

        // GeoJson properties
        public double[] coordinates { get; private set; }

        public string type { get; private set; } = GeoType;
    }
}
