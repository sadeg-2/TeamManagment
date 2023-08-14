using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManagment.Infrastructure.Middlewares
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly IToastNotification _toastNotification;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GlobalExceptionFilter(IToastNotification toastNotification, IHttpContextAccessor httpContextAccessor)
        {
            _toastNotification = toastNotification;
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnException(ExceptionContext context)
        {
            var previousUrl = _httpContextAccessor.HttpContext.Request.Headers["Referer"].ToString();
            _httpContextAccessor.HttpContext.Items["PreviousUrl"] = previousUrl;
            
            _toastNotification.AddErrorToastMessage(context.Exception.Message);

            context.ExceptionHandled = true;
            context.HttpContext.Response.Redirect(previousUrl);
        }
    }

}
