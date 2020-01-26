using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using Location.Api.Models;
using Location.Api.Infrastructure.Repositories;

namespace Location.Api.Infrastructure.Services
{
    public class LocationsService : ILocationsService
    {
        private readonly ILocationsRepository repository;
        private readonly ILogger logger;

        public LocationsService(ILocationsRepository repository, ILogger<LocationsService> logger)
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
                throw new LocationDomainException("User current area not found.");

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
