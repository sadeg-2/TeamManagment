﻿global using Result = TeamManagment.Core.Constants.Results;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using TeamManagment.Infrastructure.Services.Users;
using MyUser = TeamManagment.Data.Models.User;


namespace STD.Web.Controllers
{
    // [Authorize]
    public class BaseController : Controller
    {
        protected string? userId;
        protected string? userName;

        protected MyUser? myUser;
//        private IUserService _userManager => HttpContext.RequestServices.GetService<IUserService>();
        
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //var user = context.HttpContext.User;
            //if (user.Identity.IsAuthenticated)
            //{
            //    var userName = user.Identity.Name;
            //    userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //    ViewData["UserName"] = userName;
            //    ViewData["UserId"] = userId;
            //    myUser = await _userManager.GetAsync(userName); 
            //}

            //userId = "2a27fd4e-5749-4868-9b8c-ecf1d73f582a";
            //userName = "sadeg.magde024@gmail.com";

            userId = "df0fe0e0-90d9-4cc3-87f0-ae6ccab4b0f3";
            userName = "heema@heema";

            base.OnActionExecuting(context);
        }









    }
}
