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
    public class ProgressController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ProgressController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IList<Progreso_Zona> Get(int id)
        {
            List<Progreso_Zona> list = new List<Progreso_Zona>();
            list = DatabaseManager.GetProgress(id);
            return list;
        }


       [HttpPost("{id_zona}")]
        public void Put(int id_zona)
        {
            Console.WriteLine("PUT");
            // DatabaseManager.UpdateProgress(progreso_Zona);
        }

    
    }
}
