using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using Microsoft.Extensions.Options;

using Location.Api.Models;

namespace Location.Api.Infrastructure.Repositories
{
    public class LocationsRepository : ILocationsRepository
    {
        private readonly LocationContext context;

        public LocationsRepository(IOptions<LocationSettings> options)
        {
            context = new LocationContext(options);
        }

        public async Task AddUserLocationAsync(UserLocation location)
        {
            await context.UserLocations.InsertOneAsync(location);
        }

        public async ValueTask<Locations> FindAsync(int locationId)
        {
            var filter = Builders<Locations>.Filter.Eq(nameof(Locations.LocationId), locationId);
            return await context.Locations
                                .Find(filter)
                                .FirstOrDefaultAsync();
        }

        public async ValueTask<IEnumerable<Locations>> GetAllAsync()
        {
            return await context.Locations
                                .Find(new BsonDocument())
                                .ToListAsync();
        }

        public async ValueTask<IEnumerable<Locations>> GetCurrentUserRegionsAsync(Point currentPosition)
        {
            var point = GeoJson.Point(GeoJson.Geographic(currentPosition.Longitude, currentPosition.Latitude));
            var orderByDistanceQuery = new FilterDefinitionBuilder<Locations>().Near(x => x.Location, point);
            var withinAreaQuery = new FilterDefinitionBuilder<Locations>().GeoIntersects(Polygon.GeoType, point);
            var filter = Builders<Locations>.Filter.And(orderByDistanceQuery, withinAreaQuery);
            
            return await context.Locations
                                .Find(filter)
                                .ToListAsync();
        }

        public async ValueTask<UserLocation> GetUserLocationAsync(string userId)
        {
            var filter = Builders<UserLocation>.Filter.Eq(nameof(UserLocation.UserId), userId);
            return await context.UserLocations
                                .Find(filter)
                                .FirstOrDefaultAsync();
        }

        public async Task UpdateUserLocationAsync(UserLocation location)
        {
            var filter = Builders<UserLocation>.Filter.Eq(nameof(UserLocation.UserId), location.UserId);
            var options = new ReplaceOptions { IsUpsert = true };

            await context.UserLocations.ReplaceOneAsync(filter, location, options);
        }
    }
}
