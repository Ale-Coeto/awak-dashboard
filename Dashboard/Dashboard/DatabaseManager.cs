using System.Data;
using MySql.Data.MySqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.Extensions.Configuration;
using Dashboard;
using System.Text.Json;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Security.Policy;
using Dashboard.Utils.Model;

namespace Dashboard
{
	public static class DatabaseManager
	{
        private static string ConnectionString = "";

        private static MySqlConnection Connection;
        private static IConfigurationRoot _configuration;

        static DatabaseManager()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _configuration = builder.Build();
            ConnectionString = _configuration.GetConnectionString("myDb1");
            Connection = new MySqlConnection(ConnectionString);
        }

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

            string updateQuery = "UPDATE Usuario SET username = @username, cumpleanios = STR_TO_DATE(@cumpleanios, '%Y-%m-%d'), redSocial = @red, biografia = @bio WHERE ID_Usuario = @id";

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

        public static bool IsValidUserByMail(string correo)
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            string existsQuery = $"SELECT EXISTS (SELECT 1 from Usuario where correo='{correo}' LIMIT 1) as User_exists;";

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

            string query = "getUsuario";

            Usuario user = new Usuario();
            Console.WriteLine("ID query: " + id);

            try
            {

                {
                    MySqlCommand command = new MySqlCommand();
                    command.Connection = Connection;
                    command.CommandText = query;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters["@id"].Direction = ParameterDirection.Input;

                    using (MySqlDataReader reader = command.ExecuteReader())
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
                            user.Superadmin = Convert.ToBoolean(reader["superadmin"]);
                            object cumpleaniosValue = reader["cumpleanios"];

                            if (cumpleaniosValue != DBNull.Value)
                            {
                                DateTime dateTimeValue = (DateTime)reader["cumpleanios"];
                                DateOnly dateOnlyValue = DateOnly.FromDateTime(dateTimeValue);
                                user.Cumpleanios = dateOnlyValue;
                            }
                            

                        }                      
                    }
                    
                    Console.WriteLine("User: " + user.Nombre);
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

        public static int GetUserIDByMail(string correo)
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            string userQuery = $"SELECT ID_usuario FROM Usuario WHERE correo='{correo}' LIMIT 1";

            try
            {

                {
                    if (IsValidUserByMail(correo))
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

        public static List<Usuario> GetUsuarios()
        {
            MySqlConnection Connection = new MySqlConnection(ConnectionString);

            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            string query = "getUsuarios";

            List<Usuario> usuarios = new List<Usuario>();

            try
            {

                {
                    MySqlCommand command = new MySqlCommand();
                    command.Connection = Connection;
                    command.CommandText = query;
                    command.CommandType = CommandType.StoredProcedure;
                   
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Usuario user = new Usuario();
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

                            usuarios.Add(user);

                        }
                    }
                    

                    return usuarios;

                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return usuarios;
            }

        }

        public static void UpdateAdmin(int id, bool admin)
        {
            MySqlConnection Connection = new MySqlConnection(ConnectionString);

            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            string query = "updateAdmin";

            try
            {

                {
                    MySqlCommand command = new MySqlCommand();
                    command.Connection = Connection;
                    command.CommandText = query;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@admin", admin);
                    command.Parameters["@admin"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters["@id"].Direction = ParameterDirection.Input;


                    command.ExecuteNonQuery();
            Console.WriteLine("ID: " + id + " Admin: " + admin);

                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

        }

        public static List<Progreso_Zona> GetProgress(int id) {
            List<Progreso_Zona> progresos = new List<Progreso_Zona>();

            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            string query = "getProgreso";

            try
            {

                {
                    MySqlCommand command = new MySqlCommand();
                    command.Connection = Connection;
                    command.CommandText = query;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters["@id"].Direction = ParameterDirection.Input;


                    Console.WriteLine("ID quer: " + id);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Progreso_Zona progreso = new Progreso_Zona();
                            progreso.id_usuario = Convert.ToInt32(reader["ID_Usuario"]);
                            progreso.id_zona = Convert.ToInt32(reader["ID_Zona"]);
                            progreso.Nombre = reader["nombre"].ToString();
                            progreso.MinijuegoCompletado = Convert.ToBoolean(reader["minijuegoCompletado"]);
                            progreso.Puntaje = Convert.ToInt32(reader["puntaje"]);
                            
                            TimeSpan sqlTime = reader.GetTimeSpan(reader.GetOrdinal("tiempo"));
                            
                            progreso.Tiempo = TimeOnly.FromTimeSpan(sqlTime);
                            progreso.JefeVencido = Convert.ToBoolean(reader["jefeVencido"]);

                            progresos.Add(progreso);

                        }
                    }
                    
                    Console.WriteLine("Progresos aa: " + progresos.Count);
                    return progresos;

                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return progresos;
            }
            
        }

        public static void UpdateProgress(Progreso_Zona progreso)
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            string updateQuery = "updateProgreso";

            try
            {
                // Console.WriteLine("ID: " + progreso.ID_Usuario + " Zona: " + progreso.ID_Zona + " Minijuego: " + progreso.MinijuegoCompletado + " Puntaje: " + progreso.Puntaje + " Jefe: " + progreso.JefeVencido + " Tiempo: " + progreso.Tiempo);
                {
                    MySqlCommand command = new MySqlCommand();
                    command.Connection = Connection;
                    command.CommandText = updateQuery;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id_usuario", progreso.id_usuario);
                    command.Parameters["@id_usuario"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@id_zona", progreso.id_zona);
                    command.Parameters["@id_zona"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@minijuegoCompletado", progreso.MinijuegoCompletado);
                    command.Parameters["@minijuegoCompletado"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@puntaje", progreso.Puntaje);
                    command.Parameters["@puntaje"].Direction = ParameterDirection.Input;

                    string sqlTime = progreso.Tiempo.ToString(@"HH\:mm\:ss");
                    command.Parameters.AddWithValue("@tiempo", sqlTime);
                    command.Parameters["@tiempo"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@jefeVencido", progreso.JefeVencido);
                    command.Parameters["@jefeVencido"].Direction = ParameterDirection.Input;


                    command.ExecuteNonQuery();

                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

        }

        public static int GetPuntaje() {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            string query = "getPuntaje";


            return 1;
        }

        public static List<Zona> GetZonasByUserId(int userId)
        {
            MySqlConnection Connection = new MySqlConnection(ConnectionString);

            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            string query = "getZonasByUserId";
            List<Zona> allZones = new List<Zona>();

            try
            {

                {
                    MySqlCommand command = new MySqlCommand();
                    command.Connection = Connection;
                    command.CommandText = query;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters["@user_id"].Direction = ParameterDirection.Input;
                    Zona z;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            z = new Zona();
                            z.Name = reader["nombre"].ToString();
                            z.Description = reader["descripcion"].ToString();
                            z.ID = Convert.ToInt32(reader["ID_Zona"]);
                            allZones.Add(z);
                        }
                    }
                    return allZones;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return allZones;

        }

        public static List<MiniGame> GetMinijuegosZona(int zonaId)
        {
            MySqlConnection Connection = new MySqlConnection(ConnectionString);

            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            string query = "getMinijuegosZona";
            List<MiniGame> zoneMinigames = new List<MiniGame>();

            try
            {
                {
                    MySqlCommand command = new MySqlCommand();
                    command.Connection = Connection;
                    command.CommandText = query;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@zona_id", zonaId);
                    command.Parameters["@zona_id"].Direction = ParameterDirection.Input;
                    MiniGame m;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            m = new MiniGame();
                            m.Name = reader["nombre"].ToString();
                            m.IsBoss = Convert.ToBoolean(reader["jefe"]);
                            m.IdGame = Convert.ToInt32(reader["ID_minijuego"]);
                            zoneMinigames.Add(m);
                        }
                    }
                    return zoneMinigames;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return zoneMinigames;

        }

        public static Partida GetPartidaDatos(int minigameId, int userId)
        {
            MySqlConnection Connection = new MySqlConnection(ConnectionString);

            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            string query = "getMinijuegoDatos";
            Partida p = null;

            try
            {
                {
                    MySqlCommand command = new MySqlCommand();
                    command.Connection = Connection;
                    command.CommandText = query;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@minijuego_id", minigameId);
                    command.Parameters["@minijuego_id"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters["@user_id"].Direction = ParameterDirection.Input;

                    
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            p = new Partida();
                            p.Puntaje = Convert.ToInt32(reader["puntaje"].ToString());
                            p.Tiempo = Convert.ToInt32(reader["tiempo"]);
                            p.IdUsuario = userId;
                            p.IdMinijuego = minigameId;
                            return p;
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return p;

        }

        public static Partida SetPartida(Partida p)
        {
            MySqlConnection Connection = new MySqlConnection(ConnectionString);

            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            string query = "setPartida";
            Partida nuevaPartida = null;

            try
            {
                {
                    MySqlCommand command = new MySqlCommand();
                    command.Connection = Connection;
                    command.CommandText = query;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@minijuego_id", p.IdMinijuego);
                    command.Parameters["@minijuego_id"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@user_id", p.IdUsuario);
                    command.Parameters["@user_id"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@nuevo_puntaje", p.Puntaje);
                    command.Parameters["@nuevo_puntaje"].Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@nuevo_tiempo", p.Tiempo);
                    command.Parameters["@nuevo_tiempo"].Direction = ParameterDirection.Input;


                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            nuevaPartida = new Partida();
                            nuevaPartida.Puntaje = Convert.ToInt32(reader["puntaje"].ToString());
                            nuevaPartida.Tiempo = Convert.ToInt32(reader["tiempo"]);
                            nuevaPartida.IdUsuario = Convert.ToInt32(reader["ID_Usuario"]);
                            nuevaPartida.IdMinijuego = Convert.ToInt32(reader["ID_Minijuego"]);
                            return nuevaPartida;
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return nuevaPartida;

        }


    }

    
}

