

using System.ComponentModel.DataAnnotations;
using TeamManagment.Core.Enums;

namespace TeamManagment.Data.Models
{
    public class TeamMember : BaseEntity
    {
        public int Id { get; set; }

        public Team Team { get; set; }
        public int TeamId { get; set; }

        public User Member { get; set; }
        public string MemberId { get; set; }

        [Required]
        public TeamUserType MemberPosition { get; set; }


    }
}
