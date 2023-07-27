using System.ComponentModel.DataAnnotations;

namespace TeamManagment.Core.Dtos.Review
{
    public class CreateReviewDto
    {
        [Required]
        public string Message { get; set; }
        [Required]
        [Range(0, 100)]
        public byte Rating { get; set; }
        public string ReciverId { get; set; }
        public string ReviewerId { get; set; }
    }
}
