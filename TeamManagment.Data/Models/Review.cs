using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManagment.Data.Models
{
    public class Review : BaseEntity
    {
        public int Id { get; set; }
        public string Message { get; set; }
        [Range(0, 100)]
        public byte Rating { get; set; }
        public string ReciverId { get; set; }

        // relations
        public User Reviewr { get; set; }
        public string ReviewrId { get; set; }
        public string? ReviewerId { get; set; }

    }
}
