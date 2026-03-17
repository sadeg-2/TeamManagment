
using System.ComponentModel.DataAnnotations;

namespace TeamManagment.Data.Models
{
    public class Comment : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Message { get; set; }
        [Required]
        public string WriterId { get; set; }
        [Required,StringLength(30)]
        public string WriterUserName { get; set; }
        public Task Task { get; set; }
        public int TaskId { get; set; }



    }
}
