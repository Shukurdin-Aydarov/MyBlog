using System;

namespace Location.Api.Infrastructure
{
    public class LocationDomainException : Exception
    {
        public LocationDomainException() { }

        public LocationDomainException(string message) : base(message) { }

        public LocationDomainException(string message, Exception exception)
            : base(message, exception) { }
    }
}
