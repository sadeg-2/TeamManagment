using TeamManagment.Core.Dtos.Review;
using System.Linq.Dynamic.Core;

namespace TeamManagment.Infrastructure.Services.Reviews
{
	public class ReviewService : IReviewService
	{
		private readonly ApplicationDbContext _db;
		public ReviewService(ApplicationDbContext db)
		{
			_db = db;
		}
		public int CreateReview(CreateReviewDto dto)
		{
			var validReviewer = _db.Users.Any(x=> x.Id == dto.ReviewerId && !x.IsDelete);
			var validReciverId = _db.Users.Any(x=> x.Id == dto.ReciverId && !x.IsDelete);
			if ((dto.ReciverId == dto.ReviewerId)|| !validReviewer || !validReciverId)
			{
				throw new Exception();
			}
			var review = new Review {
                ReviewrId = dto.ReviewerId,
				Message = dto.Message,
				Rating = dto.Rating,
				ReciverId = dto.ReciverId,
				CreatedBy = dto.ReviewerId,
				CreatedAt = DateTime.Now ,
			};
			_db.Add(review);
			_db.SaveChanges();
			return review.Id;
		}

        public async Task<Response<ReviewViewModel>> GetAllReviewDatatable(Request request , string memberId)
		{
            Response<ReviewViewModel> response = new Response<ReviewViewModel>() { Draw = request.Draw };
            var data = _db.Reviews.Include(x=> x.Reviewr).Where(x=> x.ReciverId == memberId && !x.IsDelete);
            response.RecordsTotal = data.Count();

            if (request.Search.Value != null)
            {
                data = data.Where(x =>
                    x.Message.ToLower().Contains(request.Search.Value.ToLower())||
					x.Reviewr.FullName.ToLower().Contains(request.Search.Value.ToLower())
                );
            }
            response.RecordsFiltered = await data.CountAsync();

            //if (request.Order != null && request.Order.Count > 0)
            //{
            //    var sortColumn = request.Columns.ElementAt(request.Order.FirstOrDefault().Column).Name;
            //    var sortDirection = request.Order.FirstOrDefault().Dir;
            //    data = data.OrderBy(string.Concat(sortColumn, " ", sortDirection));
            //}
            response.Data = await data.Skip(request.Start).Take(request.Length).Select(x => new ReviewViewModel
            {
                Id = x.Id,
				Message = x.Message,
				Rating	= x.Rating,
				ImageUrl = x.Reviewr.ImageUrl,
				ReviewerFullName = x.Reviewr.FullName,

            }).ToListAsync();
            return response;
        }
	
	}
}
