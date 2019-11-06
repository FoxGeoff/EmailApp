using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using WebUi.Features.Messaging;

namespace WebUi.Infrastructure
{
    public class ExceptionLoggingMiddleware
    {
        private readonly IHostingEnvironment _env;
        private readonly IMessageService _messageService;
        private readonly RequestDelegate _next;

        public ExceptionLoggingMiddleware(RequestDelegate next, IHostingEnvironment env, IMessageService messageService)
        {
            _env = env;
            _messageService = messageService;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                if (_env.IsDevelopment())
                {
                    throw;
                }

                await _messageService.SendExceptionEmailAsync(e, context);
                // Redirect the user to whatever the appropriate url for an unhandled exception is for your application
                context.Response.Redirect("https://localhost:44368/About");
            }
        }
    }
}
