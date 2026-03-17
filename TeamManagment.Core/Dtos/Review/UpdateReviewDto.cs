using System.ComponentModel.DataAnnotations;

namespace TeamManagment.Core.Dtos.Review
{
    public class UpdateReviewDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        [Range(0, 100)]
        public byte Rating { get; set; }
        public string ReciverId { get; set; }
        public string ReviewerId { get; set; }
    }
}
