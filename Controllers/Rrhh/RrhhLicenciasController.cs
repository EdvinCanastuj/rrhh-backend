﻿using Microsoft.AspNetCore.Mvc;
using rrhh_backend.Data.DTOs;
using rrhh_backend.Data.Models;
using rrhh_backend.Services.Rrhh;

namespace rrhh_backend.Controllers.Rrhh
{
    [ApiController]
    [Route("api/licencias")]
    public class RrhhLicenciasController : Controller
    {
        private readonly RrhhLicenciasService _rrhhLicenciasService;

        public RrhhLicenciasController(RrhhLicenciasService rrhhLicenciasService)
        {
            _rrhhLicenciasService = rrhhLicenciasService;
        }

        [HttpGet("listar")]
        public async Task<ActionResult<List<RHLicencias>>> GetLicencias()
        {
            try
            {
                var licencias = await _rrhhLicenciasService.GetLicencias();
                return Ok(licencias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener todas las licencias: {ex.Message}");
            }
        }

        [HttpGet("listar/tipo")]
        public async Task<ActionResult<List<RHLicencias>>> GetTipoLicencias()
        {
            try
            {
                var tipo = await _rrhhLicenciasService.GetTipoLicencias();
                return Ok(tipo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener los tipos de licencias: {ex.Message}");
            }
        }

        [HttpGet("listar/estado")]
        public async Task<ActionResult<List<RHLicencias>>> GetEstadoLicencias()
        {
            try
            {
                var estado = await _rrhhLicenciasService.GetEstadoLicencias();
                return Ok(estado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener los estados de licencias: {ex.Message}");
            }
        }
        [HttpPost("crear")]
        public async Task<IActionResult> CrearLicencia([FromBody] InsertLicenciaDto licenciaDto)
        {
            try
            {
                await _rrhhLicenciasService.CrearLicencia(licenciaDto.IdColaborador, licenciaDto.IdTipoLicencia, licenciaDto.IdEstadoLicencia, licenciaDto.FechaInicio, licenciaDto.FechaFin, licenciaDto.Observaciones);
                return Ok(new { Message = "Liciencia creada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la licencia: {ex.Message}");
            }
        }

        [HttpPut("actualizar/{idLicencia}")]
        public async Task<IActionResult> ActualizarLicencia(int idLicencia, [FromBody] InsertLicenciaDto licenciaDto)
        {
            try
            {
                await _rrhhLicenciasService.ActualizarLicencia(idLicencia, licenciaDto.IdColaborador, licenciaDto.IdTipoLicencia, licenciaDto.IdEstadoLicencia, licenciaDto.FechaInicio, licenciaDto.FechaFin, licenciaDto.Observaciones);
                return Ok(new { Message = "Licencia actualizada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar la licencia: {ex.Message}");
            }
        }

    }
}
