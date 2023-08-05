
using System.ComponentModel.DataAnnotations;
using TeamManagment.Core.Enums;

namespace TeamManagment.Data.Models
{
    public class Task : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 20,MinimumLength =5)]
        public string Title { get; set; }
        [Required]
        [StringLength(maximumLength:120)]
        public string Description { get; set; }

        [Range(0,7)]
        public TaskStatee IsCompleted { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DeadLine { get; set; }
        public User Assignee { get; set; }
        public string AssigneeId { get; set; }

        public bool relatedWithAssignment { get; set; }
        public List<Comment> Comments { get; set; }

        public string NotifyMsg() {
            return $"w:One Minute to finish task :{Title}";
        }
    }
}
