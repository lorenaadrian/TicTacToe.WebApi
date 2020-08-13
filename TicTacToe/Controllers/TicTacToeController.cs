using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using TicTacToe.Models;
using TicTacToeCore.Models;
using Newtonsoft.Json;

namespace TicTacToe.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicTacToeController : ControllerBase
    {

        [HttpPost]
        [Route("game")]
        public IActionResult PostGame()
        {
            return Ok(new Models.GameTicTacToe());
        }

        [HttpPost]
        [Route("{id}/movement")]
        public ActionResult<ContentReturn> PostMoviment(GameMovement turn, string id)
        {
            ActionResulting ret = turn.Movement(id);
            if (ret.StatusAction)
            {
                return Ok(new ContentReturn
                {
                    Msg = ret.MessageAction,
                    Winner = ret.Winner
                });
            }
            else
            {
                return BadRequest(new ContentReturn
                {
                    Msg = ret.MessageAction
                });
            }
        }
    }
}
