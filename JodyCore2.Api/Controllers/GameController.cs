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
    public class GameController : Controller
    {
        private readonly IGameService gameService;

        public GameController(IGameService _gameService)
        {
            gameService = _gameService;
        }

        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetAll(int year, int firstDay, int lastDay)
        {
            try
            {
                var model = gameService.GetGames(year, firstDay, lastDay);

                return Ok(model);
            }
            catch (ApplicationException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        [Produces("application/json")]
        public IActionResult Create(int year, int day, Guid homeId, Guid awayId)
        {
            try
            {
                var result = gameService.Create(year, day, homeId, awayId);

                return Ok(result);
            }
            catch (ApplicationException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("play")]
        [Produces("application/json")]
        public IActionResult Play(Guid gameId)
        {
            try
            {
                var result = gameService.Play(gameId);

                return Ok(result);
            }
            catch (ApplicationException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
