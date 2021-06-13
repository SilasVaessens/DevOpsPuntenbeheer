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

        public void PointAdd(int AccountID, int add)
        {
            int walletID = DAL.GetWalletID(AccountID);
            DAL.AddWalletPoints(walletID, add);
        }
     }
}
