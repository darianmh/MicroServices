using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TestEveryThing.Class
{
    public class UrlMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public UrlMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        // IMessageWriter is injected into InvokeAsync
        public async Task InvokeAsync(HttpContext httpContext)
        {
            _logger.Log(LogLevel.Information, "user");
            await _next(httpContext);
        }
    }
}
