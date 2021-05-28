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
    public class AccountsController : Controller
    {
        private static List<Accounts> accounts1 = new List<Accounts>();

        [HttpPost]
        public IActionResult AddAccount (Accounts accounts)
        {
            accounts1.Add(accounts);
            return Ok(accounts1);
        }
    }
}
