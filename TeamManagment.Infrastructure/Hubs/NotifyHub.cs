using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using TeamManagment.Core.Dtos.Notifications;
using TeamManagment.Infrastructure.Services.Notifications;
using Task = System.Threading.Tasks.Task;

namespace TeamManagment.Infrastructure.Hubs
{
    public class NotifyHub : Hub
    {
        private readonly INotificationService _notificationService;
        public NotifyHub(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        public async Task SendMessage(string userId, string notificationMessage) {
            await Clients.User(userId).SendAsync("ReceiveNotification", notificationMessage);
        }
        public async Task SendNotifications(List<NotificationDto> notifications)
        {
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveNotifications", notifications);
        }
        public override async Task OnConnectedAsync()
        {
            string userId = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Fetch the list of notifications from your data source
            List<NotificationDto> notifications = _notificationService.GetAllNotifications(userId);

            // Send the notifications to the connected client
            await SendNotifications(notifications);

            await base.OnConnectedAsync();
        }


    }
}
