using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STD.Web.Controllers;
using System.Data;
using TeamManagment.Core.Dtos.Tasks;
using TeamManagment.Core.Enums;
using TeamManagment.Core.Helper;
using TeamManagment.Infrastructure.Services.Tasks;


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
                    input.AssigneeId = userId;
                    var task = await _taskService.CreateAsync(input);
                    BackgroundJob.Schedule($"{task.Id}",()=>notify(task.NotifyMsg()),task.DeadLine- DateTime.Now);
                    TempData["msg"] = Result.AddSuccessResult();
                }
                catch (Exception)
                {
                    TempData["msg"] = Result.AddFailResult();
                }
            }
            else {
                TempData["msg"] = Result.InputNotValid();
            }
            return RedirectToAction("Index");
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
                    TempData["msg"] = Result.EditSuccessResult();
                }
                catch (Exception)
                {
                    TempData["msg"] = Result.EditFailResult();
                }
            }
            else
            {
                TempData["msg"] = Result.InputNotValid();
            }
            return RedirectToAction("Index");
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
                TempData["msg"] = Result.DeleteFailResult();
            }
            TempData["msg"] = Result.DeleteSuccessResult();
            return RedirectToAction("Index");
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
                TempData["msg"] = Result.AddSuccessResult();
            }
            catch (Exception)
            {
                TempData["msg"] = Result.RecoverFailResult();
            }
            return RedirectToAction("RecycleTask");
        }
        [HttpGet]
        public IActionResult Remove(int id) {
            try
            {
                _taskService.RemoveTask(id);
            }
            catch (Exception)
            {
                TempData["msg"] = Result.DeleteFailResult();
            }
            TempData["msg"] = Result.DeleteSuccessResult();
            return RedirectToAction("RecycleTask");
        }

        public async Task<IActionResult> MarkAsComplete(int id , int status) {
            try
            {
                await _taskService.MarkAsync(id,(TaskStatee)status);
                TempData["msg"] = Result.EditFailResult();
            }
            catch (Exception)
            {
                TempData["msg"] = Result.UpdateStatusSuccessResult();
            }
            return RedirectToAction("Index");
        }

        public IActionResult notify(string msg) { 
            TempData["msg"] = msg;
            return RedirectToAction("Index");
        }

    }
}
