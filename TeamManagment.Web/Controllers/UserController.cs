using Microsoft.AspNetCore.Mvc;
using TeamManagment.Core.Helper;
using TeamManagment.Infrastructure.Services.Users;
using TeamManagment.Core.Dtos.User;
using STD.Web.Controllers;

namespace TeamManagment.Web.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
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
                    await _userService.CreateAsync(input);
                }
                catch (Exception)
                {
                    return Ok(Result.AddFailResult());
                }
                return Ok(Result.AddSuccessResult());

            }
            return PartialView("_Create");
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
                }
                catch (Exception)
                {
                    return Ok(Result.EditFailResult());
                }
                return Ok(Result.EditSuccessResult());

            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _userService.DeleteAsync(id);
            }
            catch (Exception)
            {
                return NotFound();
            }
            return Ok(Result.DeleteSuccessResult());
        }
        [HttpPost]
        public async Task<JsonResult> GetDataTableData(Request request, string x)
        {
            return Json(await _userService.GetAllForDataTable(request));
        }
    }
}
