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
    public class AccountsController : ControllerBase
    {

        [HttpPost]
        public IActionResult AddAccount (Accounts accounts)
        {
            //accounts.AddAccounts(accounts);
            return Ok(accounts);
        }

        [HttpDelete("{AccountID}")]
        public IActionResult DeleteAccount (int AccountID)
        {
            Accounts accounts = new Accounts(0, 0);
            accounts.DeleteAccounts(AccountID);
            return Ok();
        }

        [HttpGet("{AccountID}")]
        public IActionResult GetWalletPoints(int AccountID)
        {
            Accounts accounts = new Accounts(0, 0);
            int Walletpoints = accounts.GetWalletPoints(AccountID);
            return Ok(Walletpoints);
        }

        [HttpPut("{AccountID}")]
        public IActionResult ChangeWallet(Accounts accounts)
        {
            accounts.ChangeWallet(accounts);
            return Ok();
        }
    }
}
