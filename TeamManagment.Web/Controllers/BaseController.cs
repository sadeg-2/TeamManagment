global using Result = TeamManagment.Core.Constants.Results;

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
            
            userId = "a1ba4b54-0d5b-467c-9b32-c48aa6f87b34";
            userName = "sadeg.magde024@gmail.com"; 
            
            //userId = "0890e21b-bbaa-4b4f-999b-b45c861d94fd";
            //userName = "heema@heema";

            base.OnActionExecuting(context);
        }









    }
}
