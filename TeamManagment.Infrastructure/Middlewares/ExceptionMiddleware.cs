using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NToastNotify;
using System.Net;
using TeamManagment.Core.Exceptions;
using Task = System.Threading.Tasks.Task;

namespace TeamManagment.Infrastructure.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception e)
            {
                await HandleExceptionAsync(context , e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
                // Handle the exception and generate an appropriate response
            //context.Response.StatusCode = 500;
            //context.Response.ContentType = "text/plain";
            //await context.Response.WriteAsync($"An error occurred. {exception.Message}");
            var errorMessage = "An error occurred."; // Customize the error message
            context.Response.Headers["ErrorMessage"] = errorMessage;
            context.Response.Headers["Referer"] = context.Request.Headers["Referer"].ToString();


        }


    }

}
