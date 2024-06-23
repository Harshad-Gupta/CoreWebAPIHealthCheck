using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace CoreWebAPIHealthCheck
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class HealthCheckMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ConcurrentDictionary<string, Dictionary<int, int>> Dict_HealthDetails = new ConcurrentDictionary<string, Dictionary<int, int>>();

        public HealthCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            var endPoint = httpContext.GetEndpoint()?.Metadata?.GetMetadata<ControllerActionDescriptor>();

            if(endPoint != null)
            {
                var path = $"{endPoint.ControllerName}/{endPoint.ActionName}";

                httpContext.Response.OnStarting(() =>
                {
                    var statusCode = httpContext.Response.StatusCode;

                    Dict_HealthDetails.AddOrUpdate(path,
                        new Dictionary<int, int> { { statusCode, 1} },
                            (key, existingDict) =>
                            {
                                if (existingDict.ContainsKey(statusCode))
                                {
                                    existingDict[statusCode]++;
                                }
                                else
                                {
                                    existingDict[statusCode] = 1;
                                }

                                return existingDict;
                            }
                         );

                    return Task.CompletedTask;
                });
            }

            return _next(httpContext);
        }

        public static Dictionary<string, Dictionary<int, int>> GetHealthStats()
        {
            return Dict_HealthDetails.ToDictionary(entry => entry.Key, entry => new Dictionary<int, int>(entry.Value));
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    //public static class HealthCheckMiddlewareExtensions
    //{
    //    public static IApplicationBuilder UseHealthCheckMiddleware(this IApplicationBuilder builder)
    //    {
    //        return builder.UseMiddleware<HealthCheckMiddleware>();
    //    }
    //}
}
