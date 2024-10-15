using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using sda_3_online_Backend_Teamwork.src.Utils;

namespace sda_3_online_Backend_Teamwork.src.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

    
        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context)
        {

  
            _logger.LogInformation($"Incoming request: {context.Request.Method} , {context.Request.Path}");


            var stopwatch = Stopwatch.StartNew();

    
            await _next(context);

            stopwatch.Stop();

      
            _logger.LogInformation($"Outgoing request: {context.Response.StatusCode} takes ({stopwatch.ElapsedMilliseconds}ms)");

        }



    }
}