using System.Collections.Generic;
using System.Threading.Tasks;

using MyBlog.Location.Api.Models;

namespace MyBlog.Location.Api.Infrastructure.Services
{
    public interface ILocationsService
    {
        ValueTask<Locations> FindLocationAsync(int locationId);
        ValueTask<IEnumerable<Locations>> GetAllLocationsAsync();

        ValueTask<UserLocation> GetUserLocationAsync(string userId);
        Task<bool> AddOrUpdateUserLocationAsync(string userId, Point currentPosition);
    }
}
