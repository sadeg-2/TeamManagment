using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManagment.Core.ViewModels
{
	public class ColleagueViewModel
	{
		// action => view Profile so i need team id done , and want to add feedBack so need user id done ,
		// add rating value or search how can i use progress bar as a control component 
		public string Id { get; set; }
        public int TeamId { get; set; }
        public string FullName { get; set; }
		public string ImageUrl { get; set; }
		// in team
		public string Position { get; set; }
		// my rating
		public int? MyRating { get; set; }
	}
}
