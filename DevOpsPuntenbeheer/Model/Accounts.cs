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

        public Accounts(int Account, int Wallet)
        {
            AccountID = Account;
            WalletID = Wallet;
        }

        public void AddAccounts(Accounts accounts)
        {
            DAL.AddWallet();
            int wallet = DAL.GetLastWalletID(0);
            DAL.AddAccount(accounts.AccountID, wallet);
        }

        public void DeleteAccounts(int AccountID)
        {
            int wallet = DAL.GetWalletID(AccountID);
            DAL.DeleteWallet(wallet);
            DAL.DeleteAccount(AccountID);
        }

        public int GetWalletPoints(int AccountID)
        {
            int wallet = DAL.GetWalletID(AccountID);
            return DAL.GetWalletPoints(wallet);
        }

        public void ChangeWallet(Accounts accounts)
        {
            if (accounts.WalletID > 0) // wijzigen naar bestaande wallet
            {
                int TransferWalletID = DAL.GetWalletID(accounts.WalletID);
                DAL.UpdateWalletAccount(TransferWalletID, accounts.AccountID);
            }
            else // wijzigen naar nieuwe wallet
            {
                DAL.AddWallet();
                int NewWalletID = DAL.GetLastWalletID(0);
                DAL.UpdateWalletAccount(NewWalletID, accounts.AccountID);
            }
            int wallet = DAL.GetWalletID(accounts.AccountID); // haalt oude walletID op
            if (DAL.WalletIsConnected(wallet) == false) // controleer of oude wallet gekoppeld is aan andere accounts
            {
                DAL.DeleteWallet(wallet); // verwijder oude wallet als het niet gekoppeld is aan ander account
            }
        }

    }
}
