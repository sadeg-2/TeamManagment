
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TeamManagment.Data.Models
{
    public class User : IdentityUser 
    {
        [Key]
        public override string Id { get; set; }
        [Required]
        [StringLength(maximumLength: 30,MinimumLength =5)]
        public string FullName { get; set; }
        [Required]
        [Phone]
        [StringLength(maximumLength: 15,MinimumLength =7)]
        public override string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public override string Email { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public bool IsDelete { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }
        [DataType(DataType.Date)]
        public DateTime? UpdatedAt { get; set; }

        public List<Task> Tasks { get; set; }


    }
}
