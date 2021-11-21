using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace backendSGCS.Middlewares {
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class Middleware {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public Middleware(RequestDelegate next, ILoggerFactory logFactory) {
            _next = next;
            _logger = logFactory.CreateLogger("MyCustomMiddleware");
            _logger.LogInformation("MyMiddleware is started");
        }

        public async Task Invoke(HttpContext httpContext) {
            _logger.LogInformation("My Middleware is executed");
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder) {
            return builder.UseMiddleware<Middleware>();
        }
    }
}
