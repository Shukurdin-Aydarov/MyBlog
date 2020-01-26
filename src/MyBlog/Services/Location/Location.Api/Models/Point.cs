namespace Location.Api.Models
{
    public class Point
    {
        public static readonly string GeoType = "Point";

        public Point(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        public double Longitude { get; }

        public double Latitude { get; }

        public double[] Coordinates() => new[] { Longitude, Latitude };

        public string Type { get; } = GeoType;
    }
}
