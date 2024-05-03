using System;
using System.Data;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dashboard
{
	public static class DatabaseManager
	{
        private static readonly string ConnectionString = "Server=127.0.0.1;Port=3306;Database=AWAQ;Uid=root;password=acoeto24;";

        private static readonly MySqlConnection Connection = new MySqlConnection(ConnectionString);

        public static MySqlConnection GetConnection()
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }
            return Connection;
        }


        public static void CloseConnection()
        {
            if (Connection.State == ConnectionState.Open)
            {
                Connection.Close();
            }
        }

        public static void InsertUser(string nombre, string correo, string contrasenia)
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            string insertQuery = "INSERT INTO Usuario (nombre, correo, contrasenia) VALUES (@nombre, @correo, @contrasenia)";

            try
            {
                
                {
                    MySqlCommand insertCommand = new MySqlCommand(insertQuery, Connection);
                    insertCommand.Parameters.AddWithValue("@nombre", nombre);
                    insertCommand.Parameters.AddWithValue("@correo", correo);
                    insertCommand.Parameters.AddWithValue("@contrasenia", contrasenia);
                    int rowsInserted = insertCommand.ExecuteNonQuery();
                    Console.WriteLine(rowsInserted + " Rows inserted");

                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

        }

        public static bool IsValidUser(string correo, string contrasenia)
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            string existsQuery = $"SELECT EXISTS (SELECT 1 from Usuario where correo='{correo}' and contrasenia='{contrasenia}' LIMIT 1) as User_exists;";

            try
            {

                {
                    MySqlCommand existsCommand = new MySqlCommand(existsQuery, Connection);
                    bool userExists = Convert.ToBoolean(existsCommand.ExecuteScalar());
                    Console.WriteLine("User valid: " + userExists);
                    return userExists;

                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }

        }
    }
}

