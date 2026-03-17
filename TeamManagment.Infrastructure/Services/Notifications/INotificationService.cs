using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManagment.Core.Dtos.Notifications;
using Task = System.Threading.Tasks.Task;

namespace TeamManagment.Infrastructure.Services.Notifications
{
    public interface INotificationService
    {
        Task AddNotify(NotificationDto dto);
        Task PushNotify(NotificationDto notify);
        List<NotificationDto> GetAllNotifications(string userId);

    }
}
