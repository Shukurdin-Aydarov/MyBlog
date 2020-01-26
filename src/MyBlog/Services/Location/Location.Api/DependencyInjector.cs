using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using Location.Api.Infrastructure.Services;
using Location.Api.Infrastructure.Repositories;

namespace Location.Api
{
    public static class DependencyInjector
    {
        public static void Inject(IServiceCollection services)
        {
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ILocationsService, LocationsService>();
            services.AddScoped<ILocationsRepository, LocationsRepository>();
            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
