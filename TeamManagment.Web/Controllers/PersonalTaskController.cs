using Microsoft.AspNetCore.Mvc;
using STD.Web.Controllers;
using TeamManagment.Core.Dtos.Tasks;
using TeamManagment.Core.Dtos.User;
using TeamManagment.Core.Helper;
using TeamManagment.Infrastructure.Services.Tasks;

namespace TeamManagment.Web.Controllers
{
    public class PersonalTaskController : BaseController
    {
        private readonly ITaskService _taskService;
        public PersonalTaskController(ITaskService taskService)
        {
            _taskService = taskService;
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
        public async Task<IActionResult> Create([FromForm] CreateTaskDto input)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    input.AssigneeId = userId ;
                    await _taskService.CreateAsync(input);
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
        public async Task<IActionResult> Update(int id)
        {
            var user = await _taskService.GetAsync(id);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm] UpdateTaskDto input)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _taskService.UpdateAsync(input);
                }
                catch (Exception)
                {
                    return Ok(Result.EditFailResult());
                }
                return Ok(Result.EditSuccessResult());

            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _taskService.DeleteAsync(id);
            }
            catch (Exception)
            {
                return Ok(Result.DeleteFailResult());
            }
            return Ok(Result.DeleteSuccessResult());
        }
        [HttpPost]
        public async Task<JsonResult> GetDataTableData(Request request, string x)
        {
            return Json(await _taskService.GetAllForDataTable(request));
        }

        public async Task<IActionResult> MarkAsComplete(int id) {
            try
            {
                await _taskService.MarkAsync(id);
            }
            catch (Exception)
            {
                return Ok(Result.UpdateStatusSuccessResult());
            }
            return Ok(Result.EditFailResult());
        }

    }
}
