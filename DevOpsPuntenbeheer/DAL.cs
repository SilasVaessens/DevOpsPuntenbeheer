using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DevOpsPuntenbeheer
{
    public class DAL
    {
        private readonly string connString = "Server=tcp:puntenbeheer.database.windows.net,1433;Initial Catalog=DevOpsPuntenbeheerDB;Persist Security Info=False;User ID=DevOpsPuntenbeheer;Password=99Siva'02;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public void AddAccount(int AccountID, int WalletID)
        {
            using SqlConnection conn = new SqlConnection(connString);
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

        public void AddWallet()
        {
            using SqlConnection conn = new SqlConnection(connString);
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


        public int GetLastWalletID(int WalletID)
        {
            using SqlConnection conn = new SqlConnection(connString);
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


        public int GetWalletID(int AccountID)
        {
            int WalletID = new int();
            using SqlConnection conn = new SqlConnection(connString);
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

        public void DeleteWallet(int WalletID)
        {
            using SqlConnection conn = new SqlConnection(connString);
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

        public void DeleteAccount(int AccountID)
        {
            using SqlConnection conn = new SqlConnection(connString);
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

        public int GetWalletPoints(int WalletID)
        {
            using SqlConnection conn = new SqlConnection(connString);
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

        public void UpdateWalletAccount(int NewWalletID, int AccountID)
        {
            using SqlConnection conn = new SqlConnection(connString);
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

        public bool? WalletIsConnected(int OldWalletID)
        {
            bool? Connected = new bool();
            List<int> ConnectedAccounts= new List<int>();
            using SqlConnection conn = new SqlConnection(connString);
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