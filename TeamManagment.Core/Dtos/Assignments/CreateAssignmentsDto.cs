using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManagment.Core.Enums;

namespace TeamManagment.Core.Dtos.Assignments
{
    public class CreateAssignmentsDto
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

        public int MemberId { get; set; }
        
        public string? CreatorUserId { get; set; }

        [Range(0, 7)]
        public TaskStatee IsCompleted { get; set; }
       
    }
}
