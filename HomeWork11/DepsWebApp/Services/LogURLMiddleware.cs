using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DepsWebApp.Services
{
    /// <summary>
    /// Class for LogURL middleware
    /// </summary>
    public class LogUrlMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogUrlMiddleware> _logger;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="loggerFactory"></param>
        public LogUrlMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory?.CreateLogger<LogUrlMiddleware>() ??
                      throw new ArgumentNullException(nameof(loggerFactory));
        }

        /// <summary>
        /// Invoke middleware
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation(
                $"Request URL: {Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(context.Request)}");
            try
            {
                _logger.LogInformation(await GetListOfStringsFromStream(context.Response.Body));
            }
            catch
            {
                //skip
            } 

            _logger.LogInformation(await ObtainRequestBody(context.Request));
            
            await this._next(context);

        }

        private static async Task<string> ObtainRequestBody(HttpRequest request)
        {
            if (request.Body == null) return string.Empty;
            request.EnableBuffering(); 
            var encoding = GetEncodingFromContentType();
            string bodyStr;
            using (var reader = new StreamReader(request.Body, encoding, true, 1024, true))
            {
                bodyStr = await reader.ReadToEndAsync().ConfigureAwait(false);

            } 
            request.Body.Seek(0, SeekOrigin.Begin); 
            return bodyStr;
        }
        private async Task<string> GetListOfStringsFromStream(Stream requestBody)
        {

            StringBuilder builder = new StringBuilder();

            byte[] buffer = ArrayPool<byte>.Shared.Rent(4096);

            while (true)
            {
                var bytesRemaining = await requestBody.ReadAsync(buffer, offset: 0, buffer.Length);
                if (bytesRemaining == 0)
                {
                    break;
                }

                
                var encodedString = Encoding.UTF8.GetString(buffer, 0, bytesRemaining);
                builder.Append(encodedString);
            }

            ArrayPool<byte>.Shared.Return(buffer);

            var entireRequestBody = builder.ToString();


            return entireRequestBody;
        }

        private static Encoding GetEncodingFromContentType()
        {
            
            return Encoding.UTF8;
        }
    }
}