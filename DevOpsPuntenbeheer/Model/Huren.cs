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
            int walletID = DAL.GetWalletID(AccountID);
            int walletpoints = DAL.GetWalletPoints(walletID);

            if(subtract > walletpoints || subtract < 0)
            {
                return false;
            }
            else
            {
                DAL.SubtractWalletPoints(walletID, subtract);
                return true;
            }
        }
    }
}
