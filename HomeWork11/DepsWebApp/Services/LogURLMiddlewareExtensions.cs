using Microsoft.AspNetCore.Builder;

namespace DepsWebApp.Services
{
    /// <summary>
    /// Middleware for logging
    /// </summary>
    public static class LogUrlMiddlewareExtensions
    {
        /// <summary>
        /// Log urls
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseLogUrl(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LogUrlMiddleware>();
        }
    
}
}