using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DevOpsPuntenbeheer
{
    public class DAL
    {
        private readonly string connString = "Server=tcp:puntenbeheer.database.windows.net,1433;Initial Catalog=DevOpsPuntenbeheerDB;Persist Security Info=False;User ID=DevOpsPuntenbeheer;Password=99Siva'02;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public void AddAccount(int AccountID, int WalletID)
        {
            SqlConnection connection = new SqlConnection(connString);
            string query = "INSERT INTO Accounts (AccountID, WalletID) VALUES (@AccountID, @WalletID)";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@AccountID", AccountID);
            command.Parameters.AddWithValue("@WalletID", WalletID);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Records Inserted Successfully");
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
            }
            finally
            {
                connection.Close();
            }
        }

        public void AddWallet()
        {
            SqlConnection connection = new SqlConnection(connString);
            string query = "INSERT INTO Wallets (WalletPoints) VALUES (@WalletPoints)";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@WalletPoints", 0);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Records Inserted Successfully");
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
            }
            finally
            {
                connection.Close();
            }

        }
    }
}
