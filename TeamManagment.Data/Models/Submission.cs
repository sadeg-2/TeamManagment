namespace TeamManagment.Data.Models
{
    public class Submission
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public TimeSpan TimeLeft { get; set; }
        // Relations 
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
        public List<Attatchment> Attatchments { get; set; }
    }
}
