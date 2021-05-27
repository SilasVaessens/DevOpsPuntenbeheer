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
    public class HurenController : Controller
    {
        private static Huren test = new Huren();

        public IActionResult Get()
        {
            return Ok(test);
        }

    }
}
