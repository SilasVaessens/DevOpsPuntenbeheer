using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevOpsPuntenbeheer.Model
{
    public class Huren
    {
        public int AccountID { get; set; }
        public int Points { get; set; }

        public bool PointSubtract(int AccountID, int subtract)
        {
            bool exists = DAL.AccountExists(AccountID);
            if (exists == true)
            {
                int? walletID = DAL.GetWalletID(AccountID);
                if (walletID != null)
                {
                    int? walletpoints = DAL.GetWalletPoints((int)walletID);

                    if (subtract > walletpoints || subtract < 0)
                    {
                        return false;
                    }
                    else
                    {
                        return DAL.SubtractWalletPoints((int)walletID, subtract);
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
