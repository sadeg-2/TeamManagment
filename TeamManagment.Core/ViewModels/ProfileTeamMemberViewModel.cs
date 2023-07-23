using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManagment.Core.ViewModels
{
    public class ProfileTeamMemberViewModel
    {
        // id user  member
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public int NumOfTaskDone { get; set; }
        public int NumOfTeamJoined { get; set; }


    }
}
