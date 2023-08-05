using Microsoft.AspNetCore.Mvc;
using STD.Web.Controllers;
using TeamManagment.Core.Dtos.Assignments;
using TeamManagment.Core.Dtos.Tasks;
using TeamManagment.Core.Dtos.Teams;
using TeamManagment.Core.Enums;
using TeamManagment.Core.Helper;
using TeamManagment.Data.Models;
using TeamManagment.Infrastructure.Services.Teams;

namespace TeamManagment.Web.Controllers
{
    public class TeamController : BaseController
    {
        private readonly  ITeamService _teamService;
        private readonly ITeamMemberService _teamMember;
        public TeamController(ITeamService teamService,ITeamMemberService teamMember)
        {
            _teamService = teamService;
            _teamMember = teamMember;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateTeamDto input)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _teamService.CreateAsync(input,userName);
                    TempData["msg"] = Result.AddSuccessResult();

                }
                catch (Exception)
                {
                    TempData["msg"] = Result.AddFailResult();
                }
            }
            else
            {
                TempData["msg"] = Result.InputNotValid();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var team = _teamService.GetAsync(id);
            if (team == null)
            {
                return Ok(Result.EditFailResult());
            }
            return PartialView( "_UpdateProfile", team);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm] UpdateTeamDto input)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _teamService.UpdateAsync(input,userName);
                    TempData["msg"] = Result.EditSuccessResult();

                }
                catch (Exception)
                {
                    TempData["msg"] = Result.EditFailResult();
                }

            }
            else
            {
                TempData["msg"] = Result.InputNotValid();
            }
            return RedirectToAction("Index");
        }

        

        [HttpGet]
        public  IActionResult Delete(int id)
        {
            try
            {
                 _teamService.DeleteAsync(id);
            }
            catch (Exception)
            {
                return NotFound(Result.DeleteFailResult());
            }
            return Ok(Result.DeleteSuccessResult());
        }
        [HttpPost]
        public async Task<JsonResult> GetDataTableData(Request request)
        {
            return Json(await _teamService.GetAllForDataTable(request,userName));
        }
        [HttpGet]
        public IActionResult ProfileTeam(int id) {
            HttpContext.Session.SetInt32("TeamId", id);

            return View(_teamService.GetTeam(id));
        }
        public ActionResult LoadPartialView(string target )
        {
            string partialViewName = "_" + target;

            return PartialView(partialViewName);
        }

        [HttpPost]
        public async Task<JsonResult> GetDataTableForMember(Request request)
        {
            int? teamId = HttpContext.Session.GetInt32("TeamId");

            return Json(await _teamMember.GetAllForDataTable(request , (int)teamId ));
        }
        [HttpPost]
        public async Task<JsonResult> GetDataTableColleagues(Request request,int teamId) {
            return Json(await _teamService.GetDataTableColleagues(request,teamId,userId));
        }


        [HttpGet]
        public IActionResult DeleteMember(int memberId)
        {
            var teamId = (int)HttpContext.Session.GetInt32("TeamId");
            try
            {
                _teamMember.Delete(memberId,userName);
                TempData["msg"] = Result.DeleteSuccessResult();
            }
            catch (Exception) { 
                TempData["msg"] = Result.DeleteFailResult();
            }

            return RedirectToAction("ProfileTeam", new { id = teamId });

        }
        public IActionResult CreateMember()
        {
            return PartialView("_CreateMember");
        }

        [HttpPost]
        public async Task<IActionResult> CreateMember([FromForm] CreateMemberDto input)
        {
            var teamId = (int)HttpContext.Session.GetInt32("TeamId");
            if (ModelState.IsValid)
            {
                try
                {
                    input.TeamId = teamId;
                    await _teamMember.CreateAsync(input, userName);
                    TempData["msg"] = Result.AddSuccessResult();
                }
                catch (Exception)
                {
                    TempData["msg"] = Result.AddFailResult();
                }
            }
            else { 
               TempData["msg"] = Result.InputNotValid();
            }
            return RedirectToAction("ProfileTeam",new { id = teamId});
        }

        [HttpGet]
        public  IActionResult AssignTask(int id)
        {
            ViewData["MemberId"] = id;
            return PartialView("_AssignTask");
        }
        [HttpPost]
        public IActionResult AssignTask(CreateAssignmentsDto dto) {
            var teamId = (int)HttpContext.Session.GetInt32("TeamId");
            if (userId == null)
            {
                TempData["msg"] = Result.InputNotValid();
                return RedirectToAction("ProfileTeam",teamId);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _teamMember.AssignTask(dto, userId);
                    TempData["msg"] = Result.AddSuccessResult();
                }
                catch (Exception)
                {
                    TempData["msg"] = Result.AddFailResult();
                }
            }
            return RedirectToAction("ProfileTeam", new { id = teamId });
        }

        public IActionResult MyColleagues() { 
            return View(_teamMember.GetMyTeam(userId));
        }





    }
}
