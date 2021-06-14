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
    public class HurenController : ControllerBase
    {
        [HttpPut("{AccountID}")]
        public IActionResult PointSubtract(Huren subtract)
        {
            bool Allowed = subtract.PointSubtract(subtract.AccountID, subtract.Points);
            if (Allowed)
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
