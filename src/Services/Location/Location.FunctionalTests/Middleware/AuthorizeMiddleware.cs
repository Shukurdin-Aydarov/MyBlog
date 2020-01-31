using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

using MyBlog.Location.Api;

namespace MyBlog.Location.FunctionalTests.Middleware
{
    internal class AuthorizeMiddleware
    {
        public static readonly string TestUserId = "4611ce3f-380d-4db5-8d76-87a8689058ed";

        private readonly RequestDelegate next;

        public AuthorizeMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var identity = new ClaimsIdentity("cookies");
            identity.AddClaim(new Claim(Constants.IdentityClaimName, TestUserId));
            context.User.AddIdentity(identity);

            await next.Invoke(context);
        }
    }
}
