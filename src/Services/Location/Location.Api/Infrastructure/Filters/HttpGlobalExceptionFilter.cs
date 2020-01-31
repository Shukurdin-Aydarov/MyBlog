using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MyBlog.Location.Api.Infrastructure.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger logger;
        private readonly IWebHostEnvironment env;

        public HttpGlobalExceptionFilter(IWebHostEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
        {
            this.env = env;
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var error = context.Exception;
            logger.LogError(new EventId(error.HResult), error, error.Message);
            
            if (error is LocationDomainException)
            {
                var message = new ErrorMessage
                {
                    Message = error.Message,
                    MessageForDeveloper = env.IsDevelopment() ? error : null
                };
                context.Result = new BadRequestObjectResult(message);
            }
            else
            {
                var message = new ErrorMessage
                {
                    Message = " An error occur. Try it again.",
                    MessageForDeveloper = env.IsDevelopment() ? error : null
                };
                context.Result = new InternalServerErrorObjectResult(message);
            }

            context.ExceptionHandled = true;
        }
    }
}
