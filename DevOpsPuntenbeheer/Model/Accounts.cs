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

        public bool AddAccounts(Accounts accounts)
        {
            bool Exists = DAL.AccountExists(accounts.AccountID);
            if (Exists == false)
            {
                bool SuccesWallet = DAL.AddWallet();
                if (SuccesWallet == true)
                {
                    int? wallet = DAL.GetLastWalletID();
                    if (wallet != null)
                    {
                        return DAL.AddAccount(accounts.AccountID, (int)wallet);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool DeleteAccounts(int AccountID)
        {
            bool Exists = DAL.AccountExists(AccountID);
            if (Exists == true)
            {
                int? wallet = DAL.GetWalletID(AccountID);
                if (wallet != null)
                {
                    bool SuccesWallet = DAL.DeleteWallet((int)wallet);
                    if (SuccesWallet == true)
                    {
                        return DAL.DeleteAccount(AccountID);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public int? GetWalletPoints(int AccountID)
        {
            bool Exists = DAL.AccountExists(AccountID);
            if (Exists == true)
            {
                int? wallet = DAL.GetWalletID(AccountID);
                if (wallet != null)
                {
                    return DAL.GetWalletPoints((int)wallet);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public bool ChangeWallet(Accounts accounts)
        {
            bool Exists = DAL.AccountExists(accounts.AccountID);
            if (Exists == true)
            {
                int? wallet = DAL.GetWalletID(accounts.AccountID); // haalt oude walletID op
                if (wallet != null)
                {
                    if (accounts.WalletID > 0) // wijzigen naar bestaande wallet
                    {
                        int? TransferWalletID = DAL.GetWalletID(accounts.WalletID);
                        if (TransferWalletID != null || wallet != TransferWalletID)
                        {
                            DAL.UpdateWalletAccount((int)TransferWalletID, accounts.AccountID);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else // wijzigen naar nieuwe wallet
                    {
                        bool SuccesAddWallet =  DAL.AddWallet();
                        if (SuccesAddWallet == true)
                        {
                            int? NewWalletID = DAL.GetLastWalletID();
                            if (NewWalletID != null)
                            {
                                DAL.UpdateWalletAccount((int)NewWalletID, accounts.AccountID);
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }

                    if (DAL.WalletIsConnected((int)wallet) == false) // controleer of oude wallet gekoppeld is aan andere accounts
                    {
                        return DAL.DeleteWallet((int)wallet); // verwijder oude wallet als het niet gekoppeld is aan ander account
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

    }
}
