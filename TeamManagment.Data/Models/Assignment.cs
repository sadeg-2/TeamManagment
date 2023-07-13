using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManagment.Data.Models
{
    public class Assignment : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public Task Task { get; set; }
        public int TaskId { get; set; }
        public TeamMember Creator { get; set; }
        public int CreatorId { get; set; }

    }
}
