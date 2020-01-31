using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SimpleEvenBus.Abstractions;

using MyBlog.Location.Api.Models;
using MyBlog.Location.Api.Infrastructure.Repositories;
using MyBlog.Location.Api.Infrastructure.Events;

namespace MyBlog.Location.Api.Infrastructure.Services
{
    public class LocationsService : ILocationsService
    {
        private readonly ILogger logger;
        private readonly IEventBus eventBus;
        private readonly ILocationsRepository repository;

        public LocationsService(ILocationsRepository repository, IEventBus eventBus, ILogger<LocationsService> logger)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
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

            if (currentUserRegions is null || currentUserRegions.Count() is 0)
                throw new LocationDomainException("User current area not found.");

            var userLocation = await repository.GetUserLocationAsync(userId);
            userLocation ??= new UserLocation();
            userLocation.UserId = userId;
            userLocation.LocationId = currentUserRegions.First().LocationId;
            userLocation.UpdateDate = DateTimeOffset.Now;

            await repository.UpdateUserLocationAsync(userLocation);

            PublishNewUserLocationPositionEvent(userId, currentUserRegions);

            return true;
        }

        private void PublishNewUserLocationPositionEvent(string userId, IEnumerable<Locations> newLocations)
        {
            var newUserLocations = MapUserLocationDetails(newLocations);
            var @event = new UserLocationUpdatedEvent(userId, newUserLocations);

            logger.LogInformation("----- Publishing integration event: {EventId} from {AppName} - ({@Event})", @event.Id, Program.AppName, @event);

            eventBus.Publish(@event);
        }

        private IEnumerable<UserLocationDetails> MapUserLocationDetails(IEnumerable<Locations> locations)
        {
            return locations.Select(l => new UserLocationDetails
            {
                LocationId = l.LocationId,
                Code = l.Code,
                Description = l.Description
            });
        }
    }
}
