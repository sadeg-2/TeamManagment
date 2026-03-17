

using System.ComponentModel.DataAnnotations;

namespace TeamManagment.Data.Models
{
    public class Team : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:15 , MinimumLength =5)]
        public string Name { get; set; }
        [Required]
        [StringLength (maximumLength:50 , MinimumLength = 5)]
        public string Description { get; set; }
        [Required]
        public string TeamLeaderUserName { get; set; }
        public int NumOfTeamMember { get; set; }

        [Required]
        public string ImageUrl { get; set; }
        public List<TeamMember> TeamMembers { get; set; }
        public List<Assignment> Assignments { get; set; }

    }
}
