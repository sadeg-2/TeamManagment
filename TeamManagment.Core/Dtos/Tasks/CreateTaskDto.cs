using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TeamManagment.Core.Dtos.Tasks
{
    public class CreateTaskDto
    {
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 5)]
        public string Title { get; set; }
        [Required]
        [StringLength(maximumLength: 120)]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DeadLine { get; set; }
        
        public string? AssigneeId { get; set; }
    }
}
