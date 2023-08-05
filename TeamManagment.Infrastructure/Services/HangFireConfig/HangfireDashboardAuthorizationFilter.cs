using Hangfire.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManagment.Infrastructure.Services.HangFireConfig
{
	public class HangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
		public bool Authorize(DashboardContext context)
		{
            var user = context.GetHttpContext().User;
			if (user != null)
			{
	            return user.Identity.IsAuthenticated && user.IsInRole("Adminstrator");
			}
            return false;

        }
    }
}
