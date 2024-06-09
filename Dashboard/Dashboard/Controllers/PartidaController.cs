using Dashboard.Utils.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartidaController : ControllerBase
    {
        // GET api/<Partida>/5
        [HttpGet("{minigame_id}/{user_id}")]
        public Partida Get(int minigame_id, int user_id)
        {
            return DatabaseManager.GetPartidaDatos(minigame_id, user_id);
        }

        // POST api/<Partida>
        [HttpPost]
        public Partida Post([FromBody] Partida partida)
        {
            return DatabaseManager.SetPartida(partida);
        }
    }
}
