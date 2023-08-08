global using Result = TeamManagment.Core.Constants.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using TeamManagment.Infrastructure.Services.Users;
using MyUser = TeamManagment.Data.Models.User;


namespace STD.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected string? userId;
        protected string? userName;
        protected MyUser? myUser;
        private IUserService _userManager => HttpContext.RequestServices.GetService<IUserService>();
        
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.User;
            if (user.Identity.IsAuthenticated)
            {
                userName = user.Identity.Name;
                userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                myUser = _userManager.GetUser(userId);
                ViewData["UserName"] = userName;
                ViewData["UserId"] = userId;
                ViewData["ImageUrl"] = myUser.ImageUrl;
                ViewData["FullName"] = myUser.FullName;
                ViewData["Email"] = myUser.Email;
                var roles = _userManager.GetUserRole(userId);
                roles.Wait();
                ViewData["Roles"] = roles.Result;
            }

            //userId = "9fcf66d7-2bab-437e-976c-23d54607281a";
            //userName = "sadeg.magde024@gmail.com";

            //userId = "62b69cb1-63f8-4406-b2c3-391989e44fa9";
            //userName = "heema@heema";
            //myUser = _userManager.GetUser(userId);
            //ViewData["UserName"] = userName;
            //ViewData["UserId"] = userId;
            //ViewData["ImageUrl"] = myUser.ImageUrl;
            //ViewData["FullName"] = myUser.FullName;
            //ViewData["Email"] = myUser.Email;

            base.OnActionExecuting(context);

        }
        public IActionResult NotFounds() {

            return View();
        }
       

    }
}
