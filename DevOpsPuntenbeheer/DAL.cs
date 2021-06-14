using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DevOpsPuntenbeheer
{
    public static class DAL
    {
        private readonly static string connString = "Server=tcp:puntenbeheer.database.windows.net,1433;Initial Catalog=DevOpsPuntenbeheerDB;Persist Security Info=False;User ID=DevOpsPuntenbeheer;Password=99Siva'02;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private static SqlConnection conn = new SqlConnection(connString); 

        public static void AddAccount(int AccountID, int WalletID)
        {
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

        public static void AddWallet()
        {
            using SqlCommand cmd = new SqlCommand(connString);
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO Wallets (WalletPoints) VALUES (@WalletPoints)";
            cmd.Parameters.AddWithValue("@WalletPoints", 0);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Wallet Inserted Successfully");

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


        public static int GetLastWalletID(int WalletID)
        {
            using SqlCommand cmd = new SqlCommand(connString);
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Wallets WHERE WalletID=(SELECT max(WalletID) FROM Wallets)";
            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int Wallet = (int)reader["WalletID"];
                    WalletID = Wallet;
                }
            }
            catch (SqlException e)
            {

                Console.WriteLine("Error Generated. Details: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }
            return WalletID;
        }


        public static int GetWalletID(int AccountID)
        {
            int WalletID = new int();
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
            }
            finally
            {
                conn.Close();
            }
            return WalletID;
        }

        public static void DeleteWallet(int WalletID)
        {
            using SqlCommand cmd = new SqlCommand(connString);
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM Wallets WHERE WalletID = @WalletID";
            cmd.Parameters.AddWithValue("@WalletID", WalletID);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Wallet Deleted Successfully");
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

        public static void DeleteAccount(int AccountID)
        {
            using SqlCommand cmd = new SqlCommand(connString);
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM Accounts WHERE AccountID = @AccountID";
            cmd.Parameters.AddWithValue("@AccountID", AccountID);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Account Deleted Succesfully");
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

        public static int GetWalletPoints(int WalletID)
        {
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
                    int Wallet = (int)reader["WalletPoints"];
                    WalletID = Wallet;
                }
            }
            catch (SqlException e)
            {

                Console.WriteLine("Error Generated. Details: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }
            return WalletID;
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

        public static void AddWalletPoints(int WalletID, int add)
        {
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

        public static void SubtractWalletPoints(int WalletID, int subtract)
        {
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

        public static void UpdateWalletPoints(int WalletID, int points)
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
    }
}

