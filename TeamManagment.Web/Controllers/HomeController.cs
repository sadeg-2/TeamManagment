using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using STD.Web.Controllers;
using TeamManagment.Core.Dtos.Notifications;
using TeamManagment.Core.Enums;
using TeamManagment.Infrastructure.Services.Notifications;
using TeamManagment.Web.Hubs;

namespace TeamManagment.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INotificationService _notificationService;
        public HomeController(ILogger<HomeController> logger, INotificationService notificationService)
        {
            _logger = logger;
            _notificationService = notificationService;
        }


        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult hey() {
            TempData["msg"] = "e:Add Successfully";
            return RedirectToAction("Index");
        }
        public IActionResult UpgradeRole()
        {
            return View();
        }

        public async Task<IActionResult> ChatTest() {
            TempData["userId"] = userId;
            var notify = new NotificationDto { 
                UserId = "8f014522-2dab-43f4-b02a-9d364d6b0138",
                Action = NotificationAction.general,
                Message = "Heeeeeeeeeeeeeeeeeeeeeeeeeeeey",
                SendAt = DateTime.Now + TimeSpan.FromSeconds(10),
                Title = "HeOOOOOOOOOOOOO",
            };
            BackgroundJob.Schedule(
                () =>  _notificationService.PushNotify( notify),notify.SendAt
                );
            return View();
        }
       
        public IActionResult SendMessages() { 
            
            return RedirectToAction("ChatTest");
        }



    }
}