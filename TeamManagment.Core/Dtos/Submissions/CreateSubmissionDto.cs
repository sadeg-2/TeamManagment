using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TeamManagment.Core.Dtos.Submissions
{
    public class CreateSubmissionDto
    {
        public DateTime? CreatedAt { get; set; }
        public TimeSpan? TimeLeft { get; set; }
        public int AssignmentId { get; set; }
        public List<IFormFile> Attatchments { get; set; }
    }
}
