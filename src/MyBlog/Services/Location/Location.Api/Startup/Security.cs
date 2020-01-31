using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyBlog.Location.Api
{
    public static class Security
    {
        public static readonly string Audience = "locations";
        public static readonly string IdentityUrlKey = "IdentityUrl";
        
        public static void ConfigureSecurityServices(this IServiceCollection services, IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = configuration.GetValue<string>(IdentityUrlKey);
                options.Audience = Audience;
                options.RequireHttpsMetadata = false;
            });
        }

        public static IApplicationBuilder UseSecurity(this IApplicationBuilder app)
        {
            app.UseAuthentication()
               .UseAuthorization();

            return app;
        }
    }
}
