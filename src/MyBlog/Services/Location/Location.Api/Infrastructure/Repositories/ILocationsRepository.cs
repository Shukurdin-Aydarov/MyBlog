using System.Collections.Generic;
using System.Threading.Tasks;

using MyBlog.Location.Api.Models;

namespace MyBlog.Location.Api.Infrastructure.Repositories
{
    public interface ILocationsRepository
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
