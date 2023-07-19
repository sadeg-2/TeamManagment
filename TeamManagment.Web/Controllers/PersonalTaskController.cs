using Microsoft.AspNetCore.Mvc;
using STD.Web.Controllers;
using TeamManagment.Core.Dtos.Tasks;
using TeamManagment.Core.Dtos.User;
using TeamManagment.Core.Enums;
using TeamManagment.Core.Helper;
using TeamManagment.Infrastructure.Services.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TeamManagment.Web.Controllers
{
    public class PersonalTaskController : BaseController
    {
        private readonly ITaskService _taskService;
        private TaskStatee _status; 
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
            return PartialView("_Create",input);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var task = await _taskService.GetAsync(id);
            if (task == null)
            {
                return RedirectToAction("Index");
            }
            task.AssigneeId = userId;
            return PartialView("_Update", task);
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
            return NotFound();
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
        public async Task<JsonResult> GetDataTableData(Request request, int filter)
        {
            return Json(await _taskService.GetAllForDataTable(request , userId ,(TaskStatee)filter));
        }
        [HttpPost]
        public async Task<JsonResult> GetRecycleTask(Request request ) {

            return Json(await _taskService.GetAllDeletedTask(request, userId));
        }

        public IActionResult RecycleTask() {

            return View();
        }

        [HttpGet]
        public IActionResult Recover(int id) {
            try
            {
                _taskService.RecoverTask(id);
            }
            catch (Exception)
            {
                return Ok(Result.AddFailResult());
            }
            return Ok(Result.AddSuccessResult());
        }
        [HttpGet]
        public IActionResult Remove(int id) {
            try
            {
                _taskService.RemoveTask(id);
            }
            catch (Exception)
            {
                return Ok(Result.DeleteFailResult());
            }
            return Ok(Result.DeleteSuccessResult());

        }

        public async Task<IActionResult> MarkAsComplete(int id , int status) {
            try
            {
                await _taskService.MarkAsync(id,(TaskStatee)status);
            }
            catch (Exception)
            {
                return Ok(Result.UpdateStatusSuccessResult());
            }
            return Ok(Result.EditFailResult());
        }

       

    }
}
