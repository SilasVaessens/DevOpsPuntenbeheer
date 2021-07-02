using Microsoft.VisualStudio.TestTools.UnitTesting;
using DevOpsPuntenbeheer;
using DevOpsPuntenbeheer.Model;

namespace TestsPuntenbeheer
{
    [TestClass]
    public class TestIntakeProducten
    {
        [TestMethod]
        public void TestIntakeProduct()
        {
            Accounts TestAccount = new Accounts(1000,0);
            TestAccount.AddAccounts(TestAccount);
            int? LastWalletID = DAL.GetLastWalletID();
            if (LastWalletID == null)
            {
                Assert.IsTrue(false);
            }
            else
            {
                int? InitialWalletPoints = DAL.GetWalletPoints((int)LastWalletID);
                if (InitialWalletPoints == null)
                {
                    Assert.IsTrue(false);
                }
                else
                {
                    Assert.AreEqual(0, InitialWalletPoints);
                    IntakeProducten Intake = new IntakeProducten();
                    Intake.PointAdd(1000, 100);
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
