using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManagment.Data.Models
{
    public class Attatchment
    {
        public int Id { get; set; }
        [Required]
        public string AttachmentUrl { get; set; }

        // Relations 
        public int SubmissionId { get; set; }
        public Submission Submission { get; set; }

    }
}
