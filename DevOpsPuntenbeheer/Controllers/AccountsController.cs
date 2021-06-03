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

        [HttpPost]
        public IActionResult AddAccount (Accounts accounts)
        {
            accounts.AddAccounts(accounts);
            return Ok();
        }
    }
}
