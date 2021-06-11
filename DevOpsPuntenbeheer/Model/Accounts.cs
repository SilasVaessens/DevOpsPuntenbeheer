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
            int wallet = Dal.GetLastWalletID(0);
            Dal.AddAccount(accounts.AccountID, wallet);
        }

        public void DeleteAccounts(int AccountID)
        {
            int wallet = Dal.GetWalletID(AccountID);
            Dal.DeleteWallet(wallet);
            Dal.DeleteAccount(AccountID);
        }

        public int GetWalletPoints(int AccountID)
        {
            int wallet = Dal.GetWalletID(AccountID);
            return Dal.GetWalletPoints(wallet);
        }

        public void ChangeWallet(Accounts accounts)
        {
            if (accounts.WalletID > 0) // wijzigen naar bestaande wallet
            {
                int TransferWalletID = Dal.GetWalletID(accounts.WalletID);
                Dal.UpdateWalletAccount(TransferWalletID, accounts.AccountID);
            }
            else // wijzigen naar nieuwe wallet
            {
                Dal.AddWallet();
                int NewWalletID = Dal.GetLastWalletID(0);
                Dal.UpdateWalletAccount(NewWalletID, accounts.AccountID);
            }
            int wallet = Dal.GetWalletID(accounts.AccountID); // haalt oude walletID op
            if (Dal.WalletIsConnected(wallet) == false) // controleer of oude wallet gekoppeld is aan andere accounts
            {
                Dal.DeleteWallet(wallet); // verwijder oude wallet als het niet gekoppeld is aan ander account
            }
        }

    }
}
