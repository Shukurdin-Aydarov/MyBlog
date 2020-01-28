using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using Location.Api.Infrastructure.Services;
using Location.Api.Infrastructure.Repositories;

namespace Location.Api
{
    public static class DependencyInjector
    {
        public static void InjectDependencies(this IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ILocationsRepository, LocationsRepository>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<ILocationsService, LocationsService>();
        }
    }
}
