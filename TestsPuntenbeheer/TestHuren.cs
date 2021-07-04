﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using DevOpsPuntenbeheer;
using DevOpsPuntenbeheer.Model;

namespace TestsPuntenbeheer
{
    [TestClass]
    public class TestHuren
    {
        [TestMethod]
        public void TestHuur()
        {
            Accounts TestAccount = new Accounts(1000, 0);
            TestAccount.AddAccounts(TestAccount);
            int? LastWalletID = DAL.GetLastWalletID();
            if (LastWalletID == null)
            {
                Assert.IsTrue(false);
            }
            else
            {
                Huren Huur = new Huren();
                Huur.PointSubtract((int)LastWalletID, 100);
                int? InitialWalletPoints = DAL.GetWalletPoints((int)LastWalletID);
                if (InitialWalletPoints == null)
                {
                    Assert.IsTrue(false);
                }
                else
                {
                    Assert.AreEqual(100, InitialWalletPoints);
                    Huren huren = new Huren();
                    huren.PointSubtract(1000, 100);
                    int? NewWalletPoints = DAL.GetWalletPoints((int)LastWalletID);
                    if (NewWalletPoints == null)
                    {
                        Assert.IsTrue(false);
                    }
                    else
                    {
                        Assert.AreEqual(100, NewWalletPoints);
                        TestAccount.DeleteAccounts(1000);
                    }

                }

            }

        }
    }
}
