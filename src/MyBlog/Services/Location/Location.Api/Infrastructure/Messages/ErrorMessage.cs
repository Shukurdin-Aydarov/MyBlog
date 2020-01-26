using System;

namespace Location.Api.Infrastructure
{
    public class ErrorMessage
    {
        public string Message { get; set; }

        // Only for development
        public Exception MessageForDeveloper { get; set; }
    }
}
