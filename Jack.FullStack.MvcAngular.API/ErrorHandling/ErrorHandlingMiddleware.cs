﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net;
using Serilog;
using Newtonsoft.Json;
using Serilog;
using Autofac;

namespace Jack.FullStack.MvcAngular.API.ErrorHandling
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        // private readonly ILogger logger;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context) // You can inject other autofac dependencies here 
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var logger = Startup.ApplicationContainer.Resolve<ILogger>();
                await HandleExceptionAsync(context, logger, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, ILogger logger, Exception exception)
        {
            
            logger.Error(exception, $"Error Handling Middleware: {exception.Message}");

            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            //if (exception is MyNotFoundException) code = HttpStatusCode.NotFound;
            //else if (exception is MyUnauthorizedException) code = HttpStatusCode.Unauthorized;
            //else if (exception is MyException) code = HttpStatusCode.BadRequest;

            var result = JsonConvert.SerializeObject(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
