using Microsoft.AspNetCore.Mvc;
using STD.Web.Controllers;
using TeamManagment.Core.Dtos.Assignments;
using TeamManagment.Core.Dtos.Comments;
using TeamManagment.Core.Dtos.Review;
using TeamManagment.Core.Helper;
using TeamManagment.Infrastructure.Services.Comments;
using TeamManagment.Infrastructure.Services.Reviews;
using TeamManagment.Infrastructure.Services.Teams;
using TeamManagment.Infrastructure.Services.Users;

namespace TeamManagment.Web.Controllers
{
    public class TeamMemberController : BaseController
    {
        private readonly ITeamMemberService _memberService;
        private readonly ICommentService _commetnService;
        private readonly IUserService _userService;
        private readonly IReviewService _reviewService;
        public TeamMemberController(ITeamMemberService memberService,
                                    ICommentService commetnService,
                                    IUserService userService,
                                    IReviewService reviewService)
        {
            _memberService = memberService;
            _commetnService = commetnService;
            _userService = userService;
            _reviewService = reviewService;
        }
        public IActionResult MyProfile()
        {
            var profile = _memberService.GetMyProfile(userId);
            return View(profile);
        }
        [HttpGet]
        public IActionResult Profile(string id)
        {
            var profile = _memberService.GetMyProfile(id);

            return View(profile);
        }
        public IActionResult MyAssignment()
        {
            return View(_memberService.GetMyTeam(userId));
        }

        public async Task<JsonResult> GetDataTableAssignment(Request request, int teamId)
        {

            return Json(await _memberService.GetAllAssignmentData(request, teamId, userId));
        }
        // id assignment
        public IActionResult ShowAssignment(int id)
        {
            var user = _userService.GetUser(userId);
            ViewData["Image"] = user.ImageUrl;
            ViewData["UserName"] = userName;
            return View(_memberService.GetAssignment(id));
        }
        [HttpGet]
        public IActionResult CreateSubmission()
        {
            var ct = new CreateAssignmentsDto
            {

                Title = "asdfafsd"
            };
            return PartialView("CreateSubmission", ct);
        }
        public ActionResult LoadPartialView(string target)
        {
            var user = _userService.GetUser(userId);
            ViewData["Image"] = user.ImageUrl;
            ViewData["UserName"] = userName;
            string partialViewName = "_" + target;

            return PartialView(partialViewName);
        }
        [HttpPost]
        public JsonResult AddComment([FromForm] CreateCommentDto dto)
        {
            dto.WriterUserName = userName;
            dto.WriterId = userId;
            return Json(_commetnService.CreateComment(dto));
        }

        public JsonResult GetComments(int assignmentId)
        {
            var data = _commetnService.GetAllComments(assignmentId);
            return Json(data);
        }

        public JsonResult RemoveItem(int itemId)
        {

            return Json(_commetnService.Delete(itemId));
        }

        public async Task<JsonResult> GetDataTableReview(Request request, string memberId)
        {
            return Json(await _reviewService.GetAllReviewDatatable(request, memberId));
        }
        [HttpGet]
        public IActionResult CreateReview(string reciverId)
        {
            ViewData["ReciverId"] = reciverId;
            return PartialView("_FeedBack");
        }
        [HttpPost]
        public IActionResult CreateReviews(CreateReviewDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dto.ReviewerId = userId;
                    _reviewService.CreateReview(dto);
                    return Ok(Result.AddSuccessResult());
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }


    }
}
