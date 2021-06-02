﻿using System;
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
                Console.WriteLine("Records Inserted Successfully");
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
                Console.WriteLine("Records Inserted Successfully");
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

        public int GetWalletID(int WalletID)
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
    }
}