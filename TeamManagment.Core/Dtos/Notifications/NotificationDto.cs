using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManagment.Core.Enums;

namespace TeamManagment.Core.Dtos.Notifications
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public NotificationAction Action { get; set; }
        public string UserId { get; set; }
        public DateTime SendAt { get; set; }
    }
}
