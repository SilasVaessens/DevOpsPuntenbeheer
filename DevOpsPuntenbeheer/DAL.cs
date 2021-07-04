using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DevOpsPuntenbeheer
{
    public static class DAL
    {
        private readonly static string connString = "Server=tcp:puntenbeheer.database.windows.net,1433;Initial Catalog=DevOpsPuntenbeheerDB;Persist Security Info=False;User ID=DevOpsPuntenbeheer;Password=99Siva'02;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private readonly static SqlConnection conn = new SqlConnection(connString); 

        public static bool AddAccount(int AccountID, int WalletID)
        {
            bool Succes = new bool();
            using SqlCommand cmd = new SqlCommand(connString);
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO Accounts (AccountID, WalletID) VALUES (@AccountID, @WalletID)";
            cmd.Parameters.AddWithValue("@AccountID", AccountID);
            cmd.Parameters.AddWithValue("@WalletID", WalletID);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Account Inserted Successfully");
                Succes = true;

            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
                Succes = false;
            }
            finally
            {
                conn.Close();
            }
            return Succes;
            
        }

        public static bool AddWallet()
        {
            bool Succes = new bool();
            using SqlCommand cmd = new SqlCommand(connString);
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO Wallets (WalletPoints) VALUES (@WalletPoints)";
            cmd.Parameters.AddWithValue("@WalletPoints", 0);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Wallet Inserted Successfully");
                Succes = true;
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
                Succes = false;
            }
            finally
            {
                conn.Close();
            }
            return Succes;
        }


        public static int? GetLastWalletID()
        {
            int? WalletID = new int();
            using SqlCommand cmd = new SqlCommand(connString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Wallets WHERE WalletID=(SELECT max(WalletID) FROM Wallets)";
            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    WalletID = (int)reader["WalletID"];
                }
            }
            catch (SqlException e)
            {

                Console.WriteLine("Error Generated. Details: " + e.ToString());
                WalletID = null;
            }
            finally
            {
                conn.Close();
            }
            return WalletID;
        }


        public static int? GetWalletID(int AccountID)
        {
            int? WalletID = new int();
            using SqlCommand cmd = new SqlCommand(connString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Accounts WHERE AccountID = @AccountID";
            cmd.Parameters.AddWithValue("@AccountID", AccountID);
            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    WalletID = (int)reader["WalletID"];
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
                WalletID = null;
            }
            finally
            {
                conn.Close();
            }
            return WalletID;
        }

        public static bool DeleteWallet(int WalletID)
        {
            bool Succes = new bool();
            using SqlCommand cmd = new SqlCommand(connString);
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM Wallets WHERE WalletID = @WalletID";
            cmd.Parameters.AddWithValue("@WalletID", WalletID);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Wallet Deleted Successfully");
                Succes = true;
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
                Succes = false;
            }
            finally
            {
                conn.Close();
            }
            return Succes;
        }

        public static bool DeleteAccount(int AccountID)
        {
            bool Succes = new bool();
            using SqlCommand cmd = new SqlCommand(connString);
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM Accounts WHERE AccountID = @AccountID";
            cmd.Parameters.AddWithValue("@AccountID", AccountID);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Account Deleted Succesfully");
                Succes = true;
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
                Succes = false;
            }
            finally
            {
                conn.Close();
            }
            return Succes;
        }

        public static int? GetWalletPoints(int WalletID)
        {
            int? WalletPoints = new int();
            using SqlCommand cmd = new SqlCommand(connString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Wallets WHERE WalletID = @WalletID";
            cmd.Parameters.AddWithValue("@WalletID", WalletID);
            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    WalletPoints = (int)reader["WalletPoints"];
                }
            }
            catch (SqlException e)
            {

                Console.WriteLine("Error Generated. Details: " + e.ToString());
                WalletPoints = null;
            }
            finally
            {
                conn.Close();
            }
            return WalletPoints;
        }


        public static void UpdateWalletAccount(int NewWalletID, int AccountID)
        {
            using SqlCommand cmd = new SqlCommand(connString);
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE Accounts SET WalletID = @NewWalletID WHERE AccountID = @AccountID";
            cmd.Parameters.AddWithValue("@NewWalletID", NewWalletID);
            cmd.Parameters.AddWithValue("@AccountID", AccountID);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Wallet Changed Succesfully");
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }

        }

        public static bool AddWalletPoints(int WalletID, int add)
        {
            bool Succes = new bool();
            using SqlCommand cmd = new SqlCommand(connString);
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE Wallets SET WalletPoints = WalletPoints + @Add WHERE WalletID = @WalletID";
            cmd.Parameters.AddWithValue("@Add", add);
            cmd.Parameters.AddWithValue("@WalletID", WalletID);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Wallet points added successfully.");
                Succes = true;
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
                Succes = false;
            }
            finally
            {
                conn.Close();
            }
            return Succes;
        }

        public static bool SubtractWalletPoints(int WalletID, int subtract)
        {
            bool Succes = new bool();
            using SqlCommand cmd = new SqlCommand(connString);
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE Wallets SET WalletPoints = WalletPoints - @Subtract WHERE WalletID = @WalletID";
            cmd.Parameters.AddWithValue("@Subtract", subtract);
            cmd.Parameters.AddWithValue("@WalletID", WalletID);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Wallet points subtracted successfully.");
                Succes = true;
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
                Succes = false;
            }
            finally
            {
                conn.Close();
            }
            return Succes;
        }

        public static void UpdateWalletPoints(int WalletID, int points) // not really needed, but is used for testing
        {
            using SqlCommand cmd = new SqlCommand(connString);
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE Wallets SET WalletPoints = @Points WHERE WalletID = @WalletID";
            cmd.Parameters.AddWithValue("@Points", points);
            cmd.Parameters.AddWithValue("@WalletID", WalletID);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Wallet points subtracted successfully.");
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        public static bool? WalletIsConnected(int OldWalletID)
        {
            bool? Connected = new bool();
            List<int> ConnectedAccounts= new List<int>();
            using SqlCommand cmd = new SqlCommand(connString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Accounts WHERE EXISTS (SELECT * FROM Accounts WHERE WalletID = @OldWalletID )";
            cmd.Parameters.AddWithValue("@OldWalletID", OldWalletID);
            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int WalletID = (int)reader["WalletID"];
                    ConnectedAccounts.Add(WalletID);
                }
                if (ConnectedAccounts.Count > 0)
                {
                    Connected = true;
                }
                else
                {
                    Connected = false;                }
            }
            catch (SqlException e)
            {

                Console.WriteLine("Error Generated. Details: " + e.ToString());
                Connected = null;
            }
            finally
            {
                conn.Close();
            }
            return Connected;
        }

        public static bool AccountExists(int AccountID)
        {
            bool Exists = new bool();
            List<int> ExistingAccounts = new List<int>();
            using SqlCommand cmd = new SqlCommand(connString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Accounts WHERE EXISTS (SELECT * FROM Accounts WHERE AccountID = @AccountID )";
            cmd.Parameters.AddWithValue("@AccountID", AccountID);
            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int Account_ID = (int)reader["AccountID"];
                    ExistingAccounts.Add(Account_ID);
                }
                if (ExistingAccounts.Count > 0)
                {
                    Exists = true;
                }
                else
                {
                    Exists = false;
                }
            }
            catch (SqlException e)
            {

                Console.WriteLine("Error Generated. Details: " + e.ToString());
                Exists = false;
            }
            finally
            {
                conn.Close();
            }

            return Exists;
        }

        public static int? GetLastAccountID() // for testing only
        {
            int? AccountID = new int();
            using SqlCommand cmd = new SqlCommand(connString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Accounts WHERE AccountID=(SELECT max(AccountID) FROM Accounts)";
            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AccountID = (int)reader["AccountID"];
                }
            }
            catch (SqlException e)
            {

                Console.WriteLine("Error Generated. Details: " + e.ToString());
                AccountID = null;
            }
            finally
            {
                conn.Close();
            }
            return AccountID;
        }

    }
}

