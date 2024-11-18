using Microsoft.AspNetCore.Mvc;
using rrhh_backend.Data.Models;
using rrhh_backend.Services.Rrhh;

namespace rrhh_backend.Controllers.Rrhh
{
    [ApiController]
    [Route("api/rrhh/estadoColaborador")]
    public class RrhhEstadoColaboradorController : Controller
    {
        private readonly RrhhEstadoColaboradorService _rrhhEstadoColaboradorService;

        public RrhhEstadoColaboradorController( RrhhEstadoColaboradorService rrhhEstadoColaboradorService)
        {
            _rrhhEstadoColaboradorService = rrhhEstadoColaboradorService;
        }
        [HttpGet("listar")]
        public async Task<ActionResult<List<RHEstadoColaborador>>> GetEstadosColaborador()
        {
            try
            {
                var estadosColaborador = await _rrhhEstadoColaboradorService.GetEstadosColaborador();
                return Ok(estadosColaborador);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener todos los estados de colaborador: {ex.Message}");
            }
        }
    }
}
