using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevOpsPuntenbeheer.Model;

namespace DevOpsPuntenbeheer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IntakeProductenController : ControllerBase
    {
        [HttpPut("{AccountID}")]
        public IActionResult PointAdd(IntakeProducten subtract)
        {
            subtract.PointAdd(subtract.AccountID, subtract.Points);
            return Ok();

        }
    }
}
