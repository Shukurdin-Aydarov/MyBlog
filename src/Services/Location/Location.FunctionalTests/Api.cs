namespace MyBlog.Location.FunctionalTests
{
    internal static class Api
    {
        internal static class Get
        {
            internal static readonly string Locations = "api/v1/locations";
            internal static string LocationBy(int id) => $"api/v1/locations/{id}";
            internal static string UserLocationBy(string id) => $"api/v1/locations/user/{id}";
        }

        internal static class Post
        {
            internal static readonly string AddNewLocation = "api/v1/locations";
        }
    }
}
