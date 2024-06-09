using Dashboard.Utils.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinigameController : ControllerBase
    {
        // GET: api/<MinigameController>
        [HttpGet("byZona/{id}")]
        public List<MiniGame> Get(int id)
        {
            return DatabaseManager.GetMinijuegosZona(id);
        }
    }
}
