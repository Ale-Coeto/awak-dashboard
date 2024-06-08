using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZoneController : ControllerBase
    {
        [HttpGet("byUserId/{id}")]
        public List<Zona> GetUserZones(int id)
        {
            return DatabaseManager.GetZonasByUserId(id);
        }
    }
}
