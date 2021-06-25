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
            bool Succes = false;
            if (Exists == false)
            {
                bool SuccesWallet = DAL.AddWallet();
                if (SuccesWallet == true)
                {
                    int? wallet = DAL.GetLastWalletID();
                    if (wallet != null)
                    {
                        Succes = DAL.AddAccount(accounts.AccountID, (int)wallet);
                        return Succes;
                    }
                    else
                    {
                        return Succes;
                    }
                }
                else
                {
                    return Succes;
                }
            }
            else
            {
                return Succes;
            }
        }

        public bool DeleteAccounts(int AccountID)
        {
            bool Exists = DAL.AccountExists(AccountID);
            bool Succes = false;
            if (Exists == true)
            {
                int? wallet = DAL.GetWalletID(AccountID);
                if (wallet != null)
                {
                    bool SuccesWallet = DAL.DeleteWallet((int)wallet);
                    if (SuccesWallet == true)
                    {
                        Succes = DAL.DeleteAccount(AccountID);
                        return Succes;
                    }
                    else
                    {
                        return Succes;
                    }
                }
                else
                {
                    return Succes;
                }
            }
            else
            {
                return Succes;
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
            bool Succes = false;
            bool Exists = DAL.AccountExists(accounts.AccountID);
            if (Exists == true)
            {
                int? wallet = DAL.GetWalletID(accounts.AccountID); // haalt oude walletID op
                if (wallet != null)
                {
                    if (accounts.WalletID > 0) // wijzigen naar bestaande wallet
                    {
                        int? TransferWalletID = DAL.GetWalletID(accounts.WalletID);
                        if (TransferWalletID != null)
                        {
                            DAL.UpdateWalletAccount((int)TransferWalletID, accounts.AccountID);
                        }
                        else
                        {
                            return Succes;
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
                                return Succes;
                            }
                        }
                        else
                        {
                            return Succes;
                        }
                    }

                    if (DAL.WalletIsConnected((int)wallet) == false) // controleer of oude wallet gekoppeld is aan andere accounts
                    {
                        Succes = DAL.DeleteWallet((int)wallet); // verwijder oude wallet als het niet gekoppeld is aan ander account
                        return Succes;
                    }
                    else
                    {
                        Succes = true;
                        return Succes;
                    }
                }
                else
                {
                    return Succes;
                }
            }
            else
            {
                return Succes;
            }
        }

    }
}
