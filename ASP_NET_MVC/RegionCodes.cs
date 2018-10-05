using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ASP_NET_MVC
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RegionCodes
    {
        private readonly RequestDelegate _next;
        string[] regions = { "UA", "RU", "US", "FR", "AU", "BY", "BR", "DE" };

        public RegionCodes(RequestDelegate next)
        {
            _next = next;
        }
        
        public Task Invoke(HttpContext httpContext)
        {
            httpContext.Items["regions"] = regions[new Random().Next(0, regions.Length)]; 
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RegionCodesExtensions
    {
        public static IApplicationBuilder UseRegionCodes(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RegionCodes>();
        }
    }
}