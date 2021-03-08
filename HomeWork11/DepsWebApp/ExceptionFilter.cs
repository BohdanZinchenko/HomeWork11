using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DepsWebApp
{
    /// <summary>
    /// Exception filter
    /// </summary>
    public class ExceptionFilter: Attribute, IExceptionFilter
    {
        struct ExceptionResult
        {
            public int Code { get; set; }
            public string Massage { get; set; }
        }

        /// <summary>
        /// Function when exception is triggered worked
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        { 
            var result = new ExceptionResult()
            {
                Code = 15,
                Massage = "Not implemented "
            };
            var jsonResult = JsonSerializer.Serialize(result);
            context.Result = new ContentResult()
            {
                Content = jsonResult
            };

                context.ExceptionHandled = true;
            }
        
    }
}