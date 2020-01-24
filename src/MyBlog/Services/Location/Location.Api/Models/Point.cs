namespace Location.Api.Models
{
    public struct Point
    {
        public Point(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;

            Type = "Point";
        }

        public double Longitude { get; }

        public double Latitude { get; }

        public double[] Coordinates() => new[] { Longitude, Latitude };

        public string Type { get; }
    }
}
