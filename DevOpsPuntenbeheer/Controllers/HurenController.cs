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
        private static List<int> test = new List<int>();

        public IActionResult Get()
        {
            test.Add(1);
            return Ok(test);
        }

    }
}
