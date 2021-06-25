using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevOpsPuntenbeheer.Model
{
    public class IntakeProducten
    {
        public int AccountID { get; set; }
        public int Points { get; set; }

        public bool PointAdd(int AccountID, int add)
        {
            bool exists = DAL.AccountExists(AccountID);
            if (exists == true)
            {
                int? walletID = DAL.GetWalletID(AccountID);
                if (walletID != null)
                {
                    return DAL.AddWalletPoints((int)walletID, add);
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
