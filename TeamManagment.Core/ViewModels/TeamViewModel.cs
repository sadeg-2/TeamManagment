using System.ComponentModel.DataAnnotations;

namespace TeamManagment.Core.ViewModels
{
    public class TeamViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TeamLeaderUserName { get; set; }
        public string ImageUrl { get; set; }
        public int NumOfTeamMember { get; set; }

    }
}
