using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

using MyBlog.Location.Api;
using MyBlog.Location.Api.Infrastructure;
using MyBlog.Location.FunctionalTests.Middleware;
using MyBlog.Location.Api.Infrastructure.Filters;
using MyBlog.Location.Api.Controllers;

namespace MyBlog.Location.FunctionalTests
{
    public class TestsStartup
    {
        public TestsStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureHelthChecks(Configuration)
                    .AddControllers(options => options.Filters.Add<HttpGlobalExceptionFilter>())
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                        JsonConvert.DefaultSettings = () => options.SerializerSettings;
                    })
                    .AddApplicationPart(typeof(LocationsController).Assembly);

            services.Configure<LocationSettings>(Configuration)
                    .InjectDependencies();

            services.Configure<RouteOptions>(Configuration);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting()
               .UseMiddleware<AuthorizeMiddleware>()
               .UseSecurity()
               .UseEndpoints(endpoints =>
               {
                   endpoints.MapControllers();
                   endpoints.MapHealthChecks();
               });

            new LocationsContextSeed(app).SeedAsync()
                                         .Wait();
        }
    }
}
