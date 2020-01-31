using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using MyBlog.Location.Api.Infrastructure.Services;
using MyBlog.Location.Api.Infrastructure.Repositories;

namespace MyBlog.Location.Api
{
    public static class DependencyInjector
    {
        public static void InjectDependencies(this IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ILocationsRepository, LocationsRepository>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<ILocationsService, LocationsService>();

            EventBusStartup.Configure(services);
        }
    }
}
