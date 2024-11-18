using Microsoft.AspNetCore.Mvc;
using rrhh_backend.Data.DTOs;
using rrhh_backend.Services.Rrhh;

namespace rrhh_backend.Controllers.Rrhh
{
    [ApiController]
    [Route("api/historialLicencia")]
    public class RrhhHistorialLicenciaController : Controller
    {
        private readonly RrhhHistorialLicenciaService _rrhhHistorialLicenciaService;

        public RrhhHistorialLicenciaController(RrhhHistorialLicenciaService rrhhHistorialLicenciaService)
        {
            _rrhhHistorialLicenciaService = rrhhHistorialLicenciaService;
        }
        [HttpGet("listar/")]
        public async Task<ActionResult<List<RHListarHistorialLicenciaDto>>> GetHistorialLicencia()
        {
            try
            {
                var historialLicencia = await _rrhhHistorialLicenciaService.GetHistorialLicencia();
                return Ok(historialLicencia);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener el historial de licencia: {ex.Message}");
            }
        }
        [HttpPost("listar/{idColaborador?}")]
        public async Task<ActionResult<List<RHListarHistorialLicenciaDto>>> GetHistorialLicenciaID([FromBody] GetDateLicencias o, int? idColaborador)
        {
            try
            {
                List<RHListarHistorialLicenciaDto> historialLicencia;

                if (idColaborador != 0)
                {
                    // Si se proporciona el idColaborador, filtrar por el colaborador
                    historialLicencia = await _rrhhHistorialLicenciaService.GetHistorialLicenciaId(idColaborador.Value, o.fechaInicio, o.fechaFin);
                }
                else
                {
                    // Si no se proporciona el idColaborador, obtener un historial general o manejar de acuerdo a la lógica de negocio
                    historialLicencia = await _rrhhHistorialLicenciaService.GetHistorialLicenciaGeneral(o.fechaInicio, o.fechaFin);
                }

                return Ok(historialLicencia);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener el historial de licencia: {ex.Message}");
            }
        }


    }
}
