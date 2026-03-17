using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManagment.Core.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string ImageUrl { get; set; }
        public string Message { get; set; }
        public string CreatedAt { get; set; }
    }
}
