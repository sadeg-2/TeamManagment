using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManagment.Core.Dtos.Review;

namespace TeamManagment.Infrastructure.Services.Reviews
{
	public interface IReviewService
	{
		int CreateReview(CreateReviewDto dto);

		Task<Response<ReviewViewModel>> GetAllReviewDatatable(Request request,string memberId);
	}
}
