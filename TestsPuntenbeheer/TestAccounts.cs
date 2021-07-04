using DevOpsPuntenbeheer;
using DevOpsPuntenbeheer.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestsPuntenbeheer
{
    [TestClass]
    public class TestAccounts
    {
        [TestMethod]
        public void TestAddAccountAndWallet()
        {
            int? LastWalletID = DAL.GetLastWalletID();

            Accounts TestAccount = new Accounts(1000, 0);
            TestAccount.AddAccounts(TestAccount);

            int? NewWalletID = DAL.GetLastWalletID();
            int? NewAccountID = DAL.GetLastAccountID();
            if (NewAccountID == null || LastWalletID == null || NewWalletID == null)
            {
                Assert.IsTrue(false);
            }
            else
            {
                Assert.AreEqual(1000, NewAccountID);
                Assert.AreNotEqual(LastWalletID, NewWalletID);
                TestAccount.DeleteAccounts(TestAccount.AccountID);
            }
        }

        [TestMethod]
        public void TestDeleteAccountAndWallet()
        {
            Accounts TestAccount = new Accounts(1000, 0);
            TestAccount.AddAccounts(TestAccount);

            int? NewWalletID = DAL.GetLastWalletID();

            if (NewWalletID == null)
            {
                Assert.IsTrue(false);
            }
            else
            {
                TestAccount.DeleteAccounts(TestAccount.AccountID);

                int? LastAccountID = DAL.GetLastAccountID();
                int? LastWalletID = DAL.GetLastWalletID();

                if (LastWalletID ==null || LastAccountID == null)
                {
                    Assert.IsTrue(false);
                }
                else
                {
                    Assert.AreNotEqual(1000, LastAccountID);
                    Assert.AreNotEqual(NewWalletID, LastWalletID);
                }


            }
        }

        [TestMethod]
        public void TestGetWalletPoints()
        {
            Accounts TestAccount = new Accounts(1000, 0);
            TestAccount.AddAccounts(TestAccount);
            int? NewWalletPoints = TestAccount.GetWalletPoints(TestAccount.AccountID);
            if (NewWalletPoints == null)
            {
                Assert.IsTrue(false);
            }
            else
            {
                Assert.AreEqual(0, NewWalletPoints);

                DAL.UpdateWalletPoints(TestAccount.AccountID, 100);
                int? UpdatedWalletPoints = TestAccount.GetWalletPoints(TestAccount.AccountID);
                if (UpdatedWalletPoints == null)
                {
                    Assert.IsTrue(false);
                }
                else
                {
                    Assert.AreEqual(100, UpdatedWalletPoints);
                    TestAccount.DeleteAccounts(TestAccount.AccountID);
                }
            }
        }

        [TestMethod]
        public void TestChangeWalletToOtherWallet()
        {
            Accounts TestAccount1 = new Accounts(1000, 0);
            TestAccount1.AddAccounts(TestAccount1);

            Accounts TestAccount2 = new Accounts(1001, 0);
            TestAccount2.AddAccounts(TestAccount2);

            int? Account1Points = TestAccount1.GetWalletPoints(TestAccount1.AccountID);
            int? Account2Points = TestAccount2.GetWalletPoints(TestAccount2.AccountID);

            if (Account1Points == null || Account2Points == null)
            {
                Assert.IsTrue(false);
            }
            else
            {
                Assert.AreEqual(0, Account1Points);
                Assert.AreEqual(0, Account2Points);

                DAL.UpdateWalletPoints(TestAccount2.AccountID, 100);

                TestAccount1.ChangeWallet(new Accounts(TestAccount1.AccountID, TestAccount2.AccountID));
                Account1Points = TestAccount1.GetWalletPoints(TestAccount1.AccountID);
                Account2Points = TestAccount2.GetWalletPoints(TestAccount2.AccountID);

                if (Account1Points == null || Account2Points == null)
                {
                    Assert.IsTrue(false);
                }
                else
                {
                    Assert.AreEqual(Account1Points, Account2Points);
                    Assert.AreEqual(100, Account1Points);
                    Assert.AreEqual(100, Account2Points);

                    TestAccount1.DeleteAccounts(TestAccount1.AccountID);
                    TestAccount2.DeleteAccounts(TestAccount2.AccountID);
                }
            }
            
        }

        [TestMethod]
        public void TestChangeWalletToNewWallet()
        {
            Accounts TestAccount = new Accounts(1000, 0);
            TestAccount.AddAccounts(TestAccount);
            int? OldWalletID = DAL.GetLastWalletID();
            if (OldWalletID == null)
            {
                Assert.IsTrue(false);
            }
            else
            {
                TestAccount.ChangeWallet(new Accounts(TestAccount.AccountID, 0));
                int? NewWalletID = DAL.GetLastWalletID();
                if (NewWalletID == null)
                {
                    Assert.IsTrue(false);
                }
                else
                {
                    Assert.AreNotEqual(OldWalletID, NewWalletID);
                    TestAccount.DeleteAccounts(TestAccount.AccountID);
                }
            }
        }

    }
}
