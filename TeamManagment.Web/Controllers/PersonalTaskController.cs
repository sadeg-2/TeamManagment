using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
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
        private readonly IToastNotification _toastNotification;


        public PersonalTaskController(ITaskService taskService,IToastNotification toastNotification)
        {
             _taskService = taskService;
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
        public async Task<IActionResult> Create([FromForm] CreateTaskDto input)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    input.AssigneeId = userId;
                    var task = await _taskService.CreateAsync(input);
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
                    _toastNotification.AddSuccessToastMessage(Result.EditSuccessResult());

                }
                catch (Exception)
                {
                    _toastNotification.AddErrorToastMessage(Result.EditFailResult());
                }
            }
            else
            {
                _toastNotification.AddWarningToastMessage(Result.InputNotValid());
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _taskService.DeleteAsync(id);
                _toastNotification.AddSuccessToastMessage(Result.DeleteSuccessResult());
            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage(Result.DeleteFailResult());
            }
            return Ok();
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
                _toastNotification.AddSuccessToastMessage(Result.RecoverSuccessResult());
            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage(Result.RecoverFailResult());

            }
            return Ok();
        }
        [HttpGet]
        public IActionResult Remove(int id) {
            try
            {
                _taskService.RemoveTask(id);
                _toastNotification.AddSuccessToastMessage(Result.DeleteSuccessResult());

            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage(Result.DeleteFailResult());
            }
            return Ok();
        }

        public async Task<IActionResult> MarkAsComplete(int id , int status) {
            try
            {
                await _taskService.MarkAsync(id,(TaskStatee)status);
                _toastNotification.AddSuccessToastMessage(Result.EditSuccessResult());
            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage(Result.UpdateStatusFailResult());
            }
            return RedirectToAction("Index");
        }

        public IActionResult notify(string msg) { 
            _toastNotification.AddInfoToastMessage(msg);
            return RedirectToAction("Index");
        }

    }
}