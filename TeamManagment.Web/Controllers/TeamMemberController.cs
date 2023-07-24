using Microsoft.AspNetCore.Mvc;
using STD.Web.Controllers;
using TeamManagment.Core.Dtos.Assignments;
using TeamManagment.Core.Helper;
using TeamManagment.Infrastructure.Services.Teams;

namespace TeamManagment.Web.Controllers
{
    public class TeamMemberController : BaseController
    {
        private readonly ITeamMemberService _memberService;
        public TeamMemberController(ITeamMemberService memberService)
        {
            _memberService = memberService;
        }
        public IActionResult MyProfile()
        {

            var profile = _memberService.GetMyProfile(userId);

            return View(profile);
        }

        public IActionResult MyAssignment()
        {
            return View(_memberService.GetMyTeam(userId));
        }

        public async Task<JsonResult> GetDataTableAssignment(Request request ,int teamId)
        {

            return Json(await _memberService.GetAllAssignmentData(request, teamId,userId));
        }
        // id assignment
        public IActionResult ShowAssignment(int id) {


            return View(_memberService.GetAssignment(id));
        }
        [HttpGet]
        public IActionResult CreateSubmission()
        {
            var ct = new CreateAssignmentsDto
            {

                Title = "asdfafsd"
            };
            return PartialView("CreateSubmission",ct);
        }
        public ActionResult LoadPartialView(string target)
        {
            string partialViewName = "_" + target;

            return PartialView(partialViewName);
        }

    }
}
