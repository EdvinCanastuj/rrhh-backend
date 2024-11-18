using Microsoft.AspNetCore.Mvc;
using rrhh_backend.Data.DTOs;
using rrhh_backend.Services.Rrhh;

namespace rrhh_backend.Controllers.Rrhh
{
    [ApiController]
    [Route("api/historialDepartamentoColaborador")]
    public class RrhhHistorialDepartamentoController : Controller
    {
        private readonly RrhhHistorialColaboradorService _rrhhhistorialColaboradoService;

        public RrhhHistorialDepartamentoController(RrhhHistorialColaboradorService rrhhHistorialColaboradorService)
        {
            _rrhhhistorialColaboradoService = rrhhHistorialColaboradorService;
        }
        [HttpGet("listar/")]
        public async Task<ActionResult<List<RHListarHistorialDepartamentoDto>>> GetHistorialDepartamento()
        {
            try
            {
                var historialDepartamento = await _rrhhhistorialColaboradoService.GetHistorialDepartamento();
                return Ok(historialDepartamento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener el historial de departamento: {ex.Message}");
            }
        }

        [HttpGet("listar/{idColaborador}")]
        public async Task<ActionResult<List<RHListarHistorialDepartamentoDto>>> GetHistorialDepartamentoID(int idColaborador)
        {
            try
            {
                var historialDepartamento = await _rrhhhistorialColaboradoService.GetHistorialDepartamentoID(idColaborador);
                return Ok(historialDepartamento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener el historial de departamento: {ex.Message}");
            }
        }

    }
}
