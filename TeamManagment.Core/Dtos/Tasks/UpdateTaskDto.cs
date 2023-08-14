
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TeamManagment.Core.ModelBinders;

namespace TeamManagment.Core.Dtos.Tasks
{
    public class UpdateTaskDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 5)]
        public string Title { get; set; }
        [Required]
        [StringLength(maximumLength: 120)]
        [ModelBinder(BinderType = typeof(HtmlEncodedModelBinder))]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DeadLine { get; set; }
        public string AssigneeId { get; set; }
        public bool IsComplete { get; set; }

    }
}
