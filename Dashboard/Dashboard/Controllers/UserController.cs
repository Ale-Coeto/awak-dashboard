using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using Org.BouncyCastle.Bcpg;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UsuariosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

// publicarprogreso y retornarlo

        // GET: api/<ValuesController>
        [HttpGet]
        public IList<Usuario> Get()
        {
            List<Usuario> allB = new List<Usuario>();
            return allB;
            //string connectionString = _configuration.GetConnectionString("myDb1");
            //using (var conexion = new MySqlConnection(connectionString))
            //{
            //    conexion.Open();

            //    MySqlCommand cmd = new MySqlCommand();
            //    cmd.Connection = conexion;
            //    cmd.CommandText = "get_all_books";
            //    cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //    Usuario bk;
            //    List<Usuario> allB = new List<Usuario>();
            //    using (var reader = cmd.ExecuteReader())
            //    {
            //        while (reader.Read())
            //        {
            //            bk = new Usuario();
            //            //bk.BookId = reader["idLibro"].ToString();
            //            //bk.Name = reader["Titulo"].ToString();
            //            //bk.Cover = reader["Portada"].ToString();
            //            //bk.Author = reader["Autor"].ToString();

            //            allB.Add(bk);
            //        }
            //    }
            //    return allB;
            //}
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IList<Usuario> Get(int id)
        {
            string connectionString = _configuration.GetConnectionString("myDb1");

            List<Usuario> allB = new List<Usuario>();

            //using (var conexion = new MySqlConnection(connectionString))
            //{
            //    conexion.Open();

            //    MySqlCommand cmd = new MySqlCommand();
            //    cmd.Connection = conexion;
            //    cmd.CommandText = "get_book_by_id";
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.AddWithValue("@bookId", id);
            //    cmd.Parameters["@bookId"].Direction = ParameterDirection.Input;

            //    Usuario bk;

            //    using (var reader = cmd.ExecuteReader())
            //    {
            //        while (reader.Read())
            //        {
            //            bk = new Usuario();
            //            //bk.BookId = reader["IdLibro"].ToString();
            //            //bk.Name = reader["Titulo"].ToString();
            //            //bk.Cover = reader["Autor"].ToString();
            //            //bk.Author = reader["Portada"].ToString();

            //            allB.Add(bk);
            //        }
            //    }
            //}
            return allB;
        }

        // POST api/<ValuesController>
        //[HttpPost]
        //public string Post([FromBody] string value)
        //{
        //    return value;
        //}

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put([FromBody] Usuario bookItem)
        {
            //string connectionString = _configuration.GetConnectionString("myDb1");
            //using (var conexion = new MySqlConnection(connectionString))
            //{
            //    conexion.Open();

            //    MySqlCommand cmd = new MySqlCommand();
            //    cmd.Connection = conexion;
            //    cmd.CommandText = "update_book";
            //    cmd.CommandType = CommandType.StoredProcedure;

            //    cmd.Parameters.AddWithValue("@bookId", bookItem.BookId);
            //    cmd.Parameters["@bookId"].Direction = ParameterDirection.Input;

            //    cmd.Parameters.AddWithValue("@bookName", bookItem.Name);
            //    cmd.Parameters["@bookName"].Direction = ParameterDirection.Input;

            //    cmd.Parameters.AddWithValue("@bookAuthor", bookItem.Author);
            //    cmd.Parameters["@bookAuthor"].Direction = ParameterDirection.Input;

            //    cmd.Parameters.AddWithValue("@bookCover", bookItem.Cover);
            //    cmd.Parameters["@bookCover"].Direction = ParameterDirection.Input;

            //    cmd.ExecuteNonQuery();
            //}
        }

        // DELETE api/<ValuesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
