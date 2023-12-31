﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using TeamManagment.Core.Validataions;

namespace TeamManagment.Core.Dtos.User
{
    public class CreateUserDto 
    {
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 5)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Required]
        [Phone]
        [StringLength(maximumLength: 15, MinimumLength = 7)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = " Email ")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Birthday")]
        public DateTime DOB { get; set; }
        [Required]
        [Display(Name = "Image")]
        [AllowedExtensions(".jpg", ".jpeg", ".png")]
        public IFormFile ImageUrl { get; set; }

    }
}
