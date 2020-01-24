using Microsoft.Extensions.Options;
using MongoDB.Driver;

using Location.Api.Models;

namespace Location.Api.Infrastructure
{
    public class LocationContext
    {
        private readonly IMongoDatabase database;

        public LocationContext(IOptions<LocationSettings> options)
        {
            var settings = options.Value;
            var client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.Database);
        }

        public IMongoCollection<UserLocation> UserLocations
        {
            get => database.GetCollection<UserLocation>("UserLocations");
        }
        
        public IMongoCollection<Locations> Locations
        {
           get => database.GetCollection<Locations>("Locations");
        }
    }
}
