using Hangfire;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManagment.Core.Dtos.Notifications;
using TeamManagment.Infrastructure.Hubs;
using Task = System.Threading.Tasks.Task;

namespace TeamManagment.Infrastructure.Services.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _db;
        private readonly IHubContext<NotifyHub> _hubContext;
        public NotificationService(ApplicationDbContext db,IHubContext<NotifyHub> hubContext)
        {
            _db = db;
            _hubContext = hubContext;
        }

        public async Task AddNotify(NotificationDto dto)
        {
            var user = _db.Users.Where(x => x.Id == dto.UserId && !x.IsDelete);
            if (user == null) {
                throw new Exception();
            }
            var notify = new Notification
            {
                Message = dto.Message,
                UserId=dto.UserId,
                Action=dto.Action,
                SendAt = dto.SendAt,
                Title = dto.Title,
            };
            _db.Add(notify);
            try
            {
                _db.SaveChanges();
            }
            catch (Exception) { 
            }
            
            BackgroundJob.Schedule(() => PushNotify(dto), dto.SendAt - DateTime.Now);
        }

        public List<NotificationDto> GetAllNotifications(string userId)
        {
            var notifies = _db.Notifications.Where(x => x.UserId == userId && x.SendAt < DateTime.Now).Select(
                x => new NotificationDto
                {
                    UserId = x.UserId,
                    Title = x.Title,
                    Action = x.Action,
                    Message = x.Message,
                    Id = x.Id,
                    SendAt = x.SendAt,
                }).OrderByDescending(x=> x.SendAt).ToList();
            return notifies;
        }

        public async Task PushNotify(NotificationDto notify ) {
            await _hubContext.Clients.User(notify.UserId).SendAsync("ReciveNotify",notify);
        
        }
    }
}