using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManagment.Core.Enums;

namespace TeamManagment.Core.Dtos.Teams
{
    public class UpdateMemberDto
    {
        public int Id { get; set; }
        [Required]
        public int TeamId { get; set; }
        [Required]
        public string MemberUserName { get; set; }
        [Required]
        public TeamUserType MemberPosition { get; set; }
        public string MemberId { get; set; }

    }
}
