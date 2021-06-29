using JodyCore2.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JodyCore2.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : Controller
    {
        private readonly ITeamService teamService;
        public TeamController(ITeamService _teamService)
        {
            teamService = _teamService;
        }

        [HttpGet("all")]
        [Produces("application/json")]
        public IActionResult GetAll()
        {
            try
            {
                var result = teamService.GetAll();
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }                       
        }

        [HttpPost("create")]
        public IActionResult Create(string name, int skill)
        {
            //validation goes here?
            try
            {
                var model = teamService.Create(name, skill);

                return Ok(model);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("update")]
        public IActionResult Update(Guid identifier, string name, int skill)
        {
            //validation goes here?
            try
            {
                var model = teamService.Save(identifier, name, skill);

                return Ok(model);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
