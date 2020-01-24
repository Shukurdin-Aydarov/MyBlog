using System.Collections.Generic;
using System.Threading.Tasks;

using Location.Api.Models;

namespace Location.Api.Infrastructure.Services
{
    public interface ILocationService
    {
        ValueTask<Locations> FindLocationAsync(int locationId);
        ValueTask<IEnumerable<Locations>> GetAllLocationsAsync();

        ValueTask<UserLocation> GetUserLocationAsync(string userId);
        Task<bool> AddOrUpdateUserLocationAsync(string userId, Point currentPosition);
    }
}
