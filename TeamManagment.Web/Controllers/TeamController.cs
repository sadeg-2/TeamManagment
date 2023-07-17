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
        private TaskStatee _status;
        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
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
            var user = _teamService.GetAsync(id);

            return View(user);
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
                return Ok(Result.DeleteFailResult());
            }
            return Ok(Result.DeleteSuccessResult());
        }
        [HttpPost]
        public async Task<JsonResult> GetDataTableData(Request request)
        {
            return Json(await _teamService.GetAllForDataTable(request));
        }
       
    }
}
