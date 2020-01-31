using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

using MyBlog.Location.Api.Infrastructure;
using MyBlog.Location.Api.Infrastructure.Filters;

namespace MyBlog.Location.Api
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
            services.ConfigureHelthChecks(Configuration)
                    .AddControllers(options => options.Filters.Add<HttpGlobalExceptionFilter>())
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                        JsonConvert.DefaultSettings = () => options.SerializerSettings;
                    });

            services.Configure<LocationSettings>(Configuration)
                    .InjectDependencies();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseRouting()
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
