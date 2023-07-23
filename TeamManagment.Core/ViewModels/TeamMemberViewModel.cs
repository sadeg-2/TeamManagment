using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManagment.Core.ViewModels
{
    public class TeamMemberViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string MemberId { get; set; }
        public string MemberPosition { get; set; }
        public string JoinDate { get; set; }

    }
}
