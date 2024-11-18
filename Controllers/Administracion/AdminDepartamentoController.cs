using Microsoft.AspNetCore.Mvc;
using rrhh_backend.Data.DTOs;
using rrhh_backend.Data.Models;
using rrhh_backend.Services.Administracion;

namespace rrhh_backend.Controllers.Administracion
{
    [ApiController]
    [Route("api/departamento")]
    public class AdminDepartamentoController : Controller
    {
        private readonly AdminDepartamentoService _departamentoService;
        public AdminDepartamentoController(AdminDepartamentoService departamentoService)
        {

            _departamentoService = departamentoService;

        }

        // [Authorize(Roles = ("CrearDepartamento"))]
        [HttpPost("crear")]
        public async Task<IActionResult> CrearDepartamento([FromBody] AdminUserDepartamentoDto nuevoDepartamento)
        {

            try
            {
                await _departamentoService.CrearDepartamento(nuevoDepartamento.nuevoDepartamento, nuevoDepartamento.descripcionDepartamento);
                return Ok(new { Message = "Departamento creado correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = $"Error al intentar crear el departamento: {ex.Message}" });
                throw;
            }
        }


        //  [Authorize(Roles = ("CrearDepartamento"))]
        [HttpGet("listar")]
        public async Task<ActionResult<List<RHDepartamento>>> GetAllDepartamentos()
        {

            try
            {
                var departamentos = await _departamentoService.GetAllDepartamentos();
                return Ok(departamentos);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // [Authorize(Roles = ("CrearDepartamento"))]
        [HttpPut("actualizar/{id}")]
        public async Task<IActionResult> ActualizarDepartamento(int id, [FromBody] AdminUserDepartamentoDto departamentoDto)
        {
            try
            {
                var departamentoExiste = await _departamentoService.FindDepartamentoById(id);
                if (departamentoExiste == null)
                {
                    return BadRequest(new { Error = $"El Departamento no existe para poder actualizar." });
                }

                await _departamentoService.ActualizarDepartamento(id, departamentoDto.nuevoDepartamento, departamentoDto.descripcionDepartamento);
                return Ok(new { Message = "Departamento actualizado correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el departamento: {ex.Message}");
            }
        }

    }
}
