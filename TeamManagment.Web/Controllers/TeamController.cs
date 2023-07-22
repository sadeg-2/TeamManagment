using Microsoft.AspNetCore.Mvc;
using STD.Web.Controllers;
using TeamManagment.Core.Dtos.Teams;
using TeamManagment.Core.Enums;
using TeamManagment.Core.Helper;
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
                }
                catch (Exception)
                {
                    return Ok(Result.AddFailResult());
                }
                return Ok(Result.AddSuccessResult());

            }
            return PartialView("_Create", input);
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
                }
                catch (Exception)
                {
                    return Ok(Result.EditFailResult());
                }
                return Ok(Result.EditSuccessResult());

            }
            return View();
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
            return Json(await _teamService.GetAllForDataTable(request));
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
        public async Task<IActionResult> CreateMember([FromForm] CreateMemberDto input)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    input.TeamId =(int)HttpContext.Session.GetInt32("TeamId");
                    await _teamMember.CreateAsync(input, userName);
                }
                catch (Exception)
                {
                    return Ok(Result.AddFailResult());
                }
                return Ok(Result.AddSuccessResult());

            }
            return PartialView("_CreateMember", input);
        }

        [HttpGet]
        public async Task<IActionResult> AssignTask(int id)
        {
            ViewData["id"] = id;
            return PartialView("_AssignTask");
        }



    }
}
