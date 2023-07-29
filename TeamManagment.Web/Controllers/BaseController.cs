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
        private IUserService _userManager => HttpContext.RequestServices.GetService<IUserService>();
        
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //var user = context.HttpContext.User;
            //if (user.Identity.IsAuthenticated)
            //{
            //    var userName = user.Identity.Name;
            //    userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //ViewData["UserName"] = userName;
            //ViewData["UserId"] = userId;
            //    myUser = await _userManager.GetAsync(userName); 
            //}

            //userId = "9fcf66d7-2bab-437e-976c-23d54607281a";
            //userName = "sadeg.magde024@gmail.com";

            userId = "62b69cb1-63f8-4406-b2c3-391989e44fa9";
            userName = "heema@heema";
            myUser = _userManager.GetUser(userId);
            ViewData["UserName"] = userName;
            ViewData["UserId"] = userId;
            ViewData["ImageUrl"] = myUser.ImageUrl;
            ViewData["FullName"] = myUser.FullName;
            ViewData["Email"] = myUser.Email;

            base.OnActionExecuting(context);

        }
    }
}
