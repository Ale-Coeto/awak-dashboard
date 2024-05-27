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

        // GET: api/<ValuesController>
        [HttpGet]
        public IList<Usuario> Get()
        {
            List<Usuario> list = new List<Usuario>();
            list = DatabaseManager.GetUsuarios();
            return list;
            
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public Usuario Get(int id)
        {
            Usuario user = new Usuario();
            user = DatabaseManager.GetUsuario(id.ToString());
            return user;
        }

    }
}
