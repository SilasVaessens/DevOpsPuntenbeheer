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
            bool Succes = accounts.AddAccounts(accounts);
            if (Succes == true)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{AccountID}")]
        public IActionResult DeleteAccount (int AccountID)
        {
            Accounts accounts = new Accounts(0, 0);
            bool Succes = accounts.DeleteAccounts(AccountID);
            if (Succes == true)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{AccountID}")]
        public IActionResult GetWalletPoints(int AccountID)
        {
            Accounts accounts = new Accounts(0, 0);
            int? Walletpoints = accounts.GetWalletPoints(AccountID);
            if (Walletpoints == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(Walletpoints);
            }
        }

        [HttpPut("{AccountID}")]
        public IActionResult ChangeWallet(Accounts accounts)
        {
            bool Succes = accounts.ChangeWallet(accounts);
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
