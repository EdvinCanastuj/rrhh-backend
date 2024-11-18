using Microsoft.AspNetCore.Mvc;
using rrhh_backend.Data.Models;
using rrhh_backend.Services.Rrhh;

namespace rrhh_backend.Controllers.Rrhh
{
    [ApiController]
    [Route("api/rrhh/estadoCivil")]
    public class RrhhEstadoCivilController : Controller
    {
        private readonly RrhhEstadoCivilService _rrhhEstadoCivilService;

        public RrhhEstadoCivilController( RrhhEstadoCivilService rrhhEstadoCivilService)
        {
            _rrhhEstadoCivilService = rrhhEstadoCivilService;
        }

        [HttpGet("listar")]
        public async Task<ActionResult<List<RHEstadoCivil>>> GetEstadosCiviles()
        {
            try
            {
                var estadosCiviles = await _rrhhEstadoCivilService.GetEstadosCiviles();
                return Ok(estadosCiviles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener todos los estados civiles: {ex.Message}");
            }
        }
    }
}
