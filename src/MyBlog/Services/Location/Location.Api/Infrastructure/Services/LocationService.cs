using Location.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Location.Api.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;

namespace Location.Api.Infrastructure.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository repository;
        private readonly ILogger logger;

        public LocationService(ILocationRepository repository, ILogger<LocationService> logger)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async ValueTask<Locations> FindLocationAsync(int locationId)
        {
            return await repository.FindAsync(locationId);
        }

        public async ValueTask<UserLocation> GetUserLocationAsync(string userId)
        {
            return await repository.GetUserLocationAsync(userId);
        }

        public async ValueTask<IEnumerable<Locations>> GetAllLocationsAsync()
        {
            return await repository.GetAllAsync();
        }

        public async Task<bool> AddOrUpdateUserLocationAsync(string userId, Point currentPosition)
        {
            var currentUserRegions = await repository.GetCurrentUserRegionsAsync(currentPosition);

            if (currentUserRegions is null)
                throw new Exception("User current area not found.");

            var userLocation = await repository.GetUserLocationAsync(userId);
            userLocation ??= new UserLocation();
            userLocation.UserId = userId;
            userLocation.LocationId = currentUserRegions.First().LocationId;
            userLocation.UpdateDate = DateTimeOffset.Now;

            await repository.UpdateUserLocationAsync(userLocation);

            //Publish event

            return true;
        }
    }
}
