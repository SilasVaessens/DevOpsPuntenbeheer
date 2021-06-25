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
            bool Succes = subtract.PointAdd(subtract.AccountID, subtract.Points);
            if (Succes == true)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
