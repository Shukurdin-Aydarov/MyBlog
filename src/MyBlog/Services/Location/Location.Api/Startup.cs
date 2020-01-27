using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

using Location.Api.Infrastructure;
using Location.Api.Infrastructure.Filters;

namespace Location.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            HealthCheck.AddHelthChecks(services, Configuration);
            services.AddControllers(options => options.Filters.Add<HttpGlobalExceptionFilter>())
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                        JsonConvert.DefaultSettings = () => options.SerializerSettings;
                    });

            services.Configure<LocationSettings>(Configuration);

            DependencyInjector.Inject(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseRouting()
               .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();

                    HealthCheck.MapHealthChecks(endpoints);
                });

            new LocationsContextSeed(app).SeedAsync()
                                        .Wait();
        }
    }
}
