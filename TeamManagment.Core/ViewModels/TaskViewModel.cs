using TeamManagment.Core.Enums;

namespace TeamManagment.Core.ViewModels
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public TaskStatee IsCompleted { get; set; }
        public string DeadLine { get; set; }
    }
}
