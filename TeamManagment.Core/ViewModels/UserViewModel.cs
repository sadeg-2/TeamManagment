﻿using System.ComponentModel.DataAnnotations;

namespace TeamManagment.Core.ViewModels
{
    public class UserViewModel
    {
        public  string Id { get; set; }
        public string FullName { get; set; }
        public  string PhoneNumber { get; set; }
        public  string Email { get; set; }
        public string DOB { get; set; }
        public string ImageUrl { get; set; }
        public string roles { get; set; }
        public bool IsDelete { get; set; }
    }
}
