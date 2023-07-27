using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManagment.Core.ViewModels
{
    public class ReviewViewModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public byte Rating { get; set; }
        public string ReviewerFullName { get; set; }
        public string ImageUrl { get; set; }
    }
}
