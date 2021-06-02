using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevOpsPuntenbeheer.Model
{
    public class Accounts
    {
        public int AccountID { get; set; }
        public int WalletID { get; set; }
        public DAL Dal { get; set; }

        public Accounts(int Account, int Wallet)
        {
            AccountID = Account;
            WalletID = Wallet;
            Dal = new DAL();
        }

        public void AddAccounts(Accounts accounts)
        {
            Dal.AddWallet();
            int wallet = Dal.GetWalletID(0);
            Dal.AddAccount(accounts.AccountID, wallet);
        }
    }
}
