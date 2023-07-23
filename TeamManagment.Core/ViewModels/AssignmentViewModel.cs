using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManagment.Core.Enums;
using TeamManagment.Core.Helper;

namespace TeamManagment.Core.ViewModels
{
    public class AssignmentViewModel
    {
        // Id Assignment
        public int Id { get; set; }
        public string Title { get; set; }
        public string TeamName { get; set; }
        public string Status { get; set; }
        public string DeadLine { get; set; }
    }
}
