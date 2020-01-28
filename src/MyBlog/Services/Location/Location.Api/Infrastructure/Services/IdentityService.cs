using System;
using Microsoft.AspNetCore.Http;

namespace Location.Api.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor accessor;

        public IdentityService(IHttpContextAccessor accessor)
        {
            this.accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
        }

        public string GetUserIdentity()
        {
            return accessor.HttpContext
                           .User
                           .FindFirst(Constants.IdentityClaimName)
                           .Value;
        }
    }
}
