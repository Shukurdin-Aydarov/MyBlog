using System.Collections.Generic;
using System.Threading.Tasks;

using Location.Api.Models;

namespace Location.Api.Infrastructure.Repositories
{
    public interface ILocationRepository
    {
        // Read only methods maybe completed synchronously
        ValueTask<Locations> FindAsync(int locationId);
        ValueTask<IEnumerable<Locations>> GetAllAsync();

        ValueTask<UserLocation> GetUserLocationAsync(string userId);
        ValueTask<IEnumerable<Locations>> GetCurrentUserRegionsAsync(Point currentPosition);

        Task AddUserLocationAsync(UserLocation location);
        Task UpdateUserLocationAsync(UserLocation location);
    }
}
