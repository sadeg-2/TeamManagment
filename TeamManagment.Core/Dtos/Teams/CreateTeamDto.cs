using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TeamManagment.Core.Dtos.Teams
{
    public class CreateTeamDto
    {
        [Required]
        [StringLength(maximumLength: 15, MinimumLength = 5)]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 5)]
        public string Description { get; set; }
        [Required]
        public IFormFile ImageUrl { get; set; }
    }
}
