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
        public IActionResult PointAdd(IntakeProducten add)
        {
            bool Succes = add.PointAdd(add.AccountID, add.Points);
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
