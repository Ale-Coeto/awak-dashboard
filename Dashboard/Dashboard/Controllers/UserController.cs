using Microsoft.AspNetCore.Mvc;

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
            return DatabaseManager.GetUsuarios();
            
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public Usuario Get(int id)
        {
            return DatabaseManager.GetUsuario(id.ToString());
        }

        // GET api/<ValuesController>/5
        [HttpGet("getId/{mail}/{password}")]
        public int GetUserId(string mail, string password)
        {
            return DatabaseManager.GetUserID(mail, password);
        }

    }
}
