using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManagment.Core.Dtos.Comments
{
    public class CreateCommentDto
    {
      
        // id assignment
        [Required]
        public int AssignmentID { get; set; }
        // from textarea
        [Required]
        public string Message { get; set; }
        // as a user
        public string WriterId { get; set; }
        public string WriterUserName { get; set; }
       

    }
}
