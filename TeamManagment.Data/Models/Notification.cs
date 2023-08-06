﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManagment.Core.Enums;

namespace TeamManagment.Data.Models
{
    public  class Notification
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Message { get; set; }
        public NotificationAction Action { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public DateTime SendAt { get; set; }
    }
}
