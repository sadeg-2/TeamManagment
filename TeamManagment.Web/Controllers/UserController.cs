using Microsoft.AspNetCore.Mvc;
using TeamManagment.Core.Helper;
using TeamManagment.Infrastructure.Services.Users;
using TeamManagment.Core.Dtos.User;
using STD.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using TeamManagment.Core.Enums;
using NToastNotify;

namespace TeamManagment.Web.Controllers
{
    [Authorize(Roles = "Adminstrator")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IToastNotification _toastNotification;

        public UserController(IUserService userService,IToastNotification toastNotification)
        {
            _userService = userService;
            _toastNotification = toastNotification;
        }

        public IActionResult Index()
        {

            return View();
        }
        public IActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateUserDto input)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _userService.CreateAsync(input, Environment.GetEnvironmentVariable("USER_PASSWORD"));
                    _toastNotification.AddSuccessToastMessage(Result.AddSuccessResult());
                }
                catch (Exception)
                {
                    _toastNotification.AddErrorToastMessage(Result.AddFailResult());
                }
            }
            else {
                _toastNotification.AddWarningToastMessage(Result.InputNotValid());
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var user = await _userService.GetAsync(id);

            return PartialView("_Update" , user);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm] UpdateUserDto input)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _userService.UpdateAsync(input);
                    _toastNotification.AddSuccessToastMessage(Result.EditSuccessResult());
                }
                catch (Exception)
                {
                    _toastNotification.AddErrorToastMessage(Result.EditFailResult());
                }
            }
            else {
                _toastNotification.AddWarningToastMessage(Result.InputNotValid());
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _userService.DeleteAsync(id);
                _toastNotification.AddSuccessToastMessage(Result.DeleteSuccessResult());
            }
            catch (Exception)
            {
                _toastNotification.AddSuccessToastMessage(Result.DeleteFailResult());
            }
            return Ok();
           // return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Recover(string id)
        {
            try
            {
                _userService.Recover(id);
                _toastNotification.AddSuccessToastMessage(Result.RecoverSuccessResult());
            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage(Result.RecoverFailResult());
            }
            return Ok();
        }
        [HttpPost]
        public async Task<JsonResult> GetDataTableData(Request request, string x)
        {
            return Json(await _userService.GetAllForDataTable(request));
        }

        [HttpPost]
        public IActionResult ChangeEmail(string email,string password) {


            return Ok();
        }
    }
}