namespace AppointmentWebApp.Web.Middleware
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using NuGet.Protocol.Plugins;

    public class SessionCheckMiddleware
    {
        private readonly RequestDelegate _next;

        public SessionCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var requestedUrl = context.Request.Path;
            if (!(requestedUrl.Value == null || requestedUrl.Value == @"/" || requestedUrl.Value == @"/Home/Login"))
            {
                // Check if session exists
                if (context.Session.Get("UserData") == null)
                {
                    context.Response.Redirect("/");
                    return;
                }
            }

            // Continue processing the request
            await _next(context);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class SessionCheckMiddlewareExtensions
    {
        public static IApplicationBuilder UseSessionCheckMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SessionCheckMiddleware>();
        }
    }

}
