using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpDelete("{AccountID}")]
        public IActionResult DeleteAccount (int AccountID)
        {
            Accounts accounts = new Accounts(0, 0);
            accounts.DeleteAccounts(AccountID);
            return Ok();
        }
    }
}
