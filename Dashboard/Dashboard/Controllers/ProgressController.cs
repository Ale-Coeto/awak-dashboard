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

// publicarprogreso y retornarlo

        // GET: api/<ValuesController>
        [HttpGet]
        public IList<Usuario> Get()
        {
            List<Usuario> allB = new List<Usuario>();
            return allB;
            
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IList<Usuario> Get(int id)
        {
            string connectionString = _configuration.GetConnectionString("myDb1");

            List<Usuario> allB = new List<Usuario>();

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
            
        }

    
    }
}
