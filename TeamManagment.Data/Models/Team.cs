

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

        public List<TeamMember> TeamMembers { get; set; }

    }
}
