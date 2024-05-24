using System.Data;
using MySql.Data.MySqlClient;


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

        public static int InsertUser(string nombre, string correo, string contrasenia)
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
                    int id = GetUserID(correo, contrasenia);
                    return id;

                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return -1;
            }

        }

        public static void UpdateUser(string id, Usuario user)
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            string updateQuery = "UPDATE Usuario SET username = @username, cumpleanios = STR_TO_DATE(@cumpleanios, '%Y-%m-%d'), redSocial = @red, biography = @bio WHERE ID_usuario = @id";

            try
            {

                {
                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, Connection);
                    Console.WriteLine("FE" + user.Username + " " + id);
                    updateCommand.Parameters.AddWithValue("@id", id);
                    updateCommand.Parameters.AddWithValue("@username", user.Username);
                    updateCommand.Parameters.AddWithValue("@red", user.RedSocial);
                    string dateString = user.Cumpleanios.ToString("yyyy-MM-dd");
                    updateCommand.Parameters.AddWithValue("@cumpleanios", dateString);
                    updateCommand.Parameters.AddWithValue("@bio", user.Bio);
                    int rowsUpdated = updateCommand.ExecuteNonQuery();
                    Console.WriteLine(rowsUpdated + " Rows updated");

                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                //return -1;
            }

        }

        public static void UpdatePassword(string id, string passcode)
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            string updateQuery = "UPDATE Usuario SET contrasenia = @contrasenia WHERE ID_usuario = @id";

            try
            {

                {
                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, Connection);
                    updateCommand.Parameters.AddWithValue("@id", id);
                    updateCommand.Parameters.AddWithValue("@contrasenia", passcode);

                    int rowsUpdated = updateCommand.ExecuteNonQuery();
                    Console.WriteLine(rowsUpdated + " Rows updated");

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

        public static Usuario GetUsuario(string id)
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            string userQuery = $"SELECT ID_usuario, contrasenia, correo, nombre, username, redSocial, biography, admin, cumpleanios, fecha_registro FROM Usuario WHERE ID_usuario='{id}' LIMIT 1";

            Usuario user = new Usuario();

            try
            {

                {
                    MySqlCommand userCommand = new MySqlCommand(userQuery, Connection);

                    using (MySqlDataReader reader = userCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user.ID_usuario = Convert.ToInt32(reader["ID_usuario"]);
                            user.Contrasenia = reader["contrasenia"].ToString();
                            user.Correo = reader["correo"].ToString();
                            user.Nombre = reader["nombre"].ToString();
                            user.Username = reader["username"].ToString();
                            user.RedSocial = reader["redSocial"].ToString();
                            user.Bio = reader["biography"].ToString();
                            user.Admin = Convert.ToBoolean(reader["admin"]);
                            object cumpleaniosValue = reader["cumpleanios"];

                            if (cumpleaniosValue != DBNull.Value)
                            {
                                DateTime dateTimeValue = (DateTime)reader["cumpleanios"];
                                DateOnly dateOnlyValue = DateOnly.FromDateTime(dateTimeValue);
                                user.Cumpleanios = dateOnlyValue;
                            }
                            

                        }                      
                    }
                    

                    return user;

                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return user;
            }

        }

        public static int GetUserID(string correo, string contrasenia)
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            string userQuery = $"SELECT ID_usuario FROM Usuario WHERE correo='{correo}' AND contrasenia='{contrasenia}' LIMIT 1";

            try
            {

                {
                    if (IsValidUser(correo, contrasenia))
                    {
                        MySqlCommand userCommand = new MySqlCommand(userQuery, Connection);
                        int id = Convert.ToInt32(userCommand.ExecuteScalar());
                        return id;

                    }
                    return -1;

                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return -1;
            }

        }

        public static string GetPassword(string id)
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            string userQuery = $"SELECT Contrasenia FROM Usuario WHERE ID_usuario='{id}' LIMIT 1";

            try
            {

                {
                    
                    MySqlCommand userCommand = new MySqlCommand(userQuery, Connection);
                    string pass = userCommand.ExecuteScalar().ToString();
                    return pass;        

                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return "null";
            }

        }
    }
}

