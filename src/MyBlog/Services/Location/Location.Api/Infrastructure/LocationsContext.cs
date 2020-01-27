using Microsoft.Extensions.Options;
using MongoDB.Driver;

using Location.Api.Models;

namespace Location.Api.Infrastructure
{
    internal class LocationsContext
    {
        private readonly IMongoDatabase database;

        public LocationsContext(IOptions<LocationSettings> options)
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
