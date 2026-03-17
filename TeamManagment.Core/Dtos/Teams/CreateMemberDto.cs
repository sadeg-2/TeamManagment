using System.ComponentModel.DataAnnotations;
using TeamManagment.Core.Enums;

namespace TeamManagment.Core.Dtos.Teams
{
    public class CreateMemberDto
    {
        [Required]
        public int TeamId { get; set; }
        [Required]
        public string MemberUserName { get; set; }
        [Required]
        public TeamUserType MemberPosition { get; set; }

    }
}
