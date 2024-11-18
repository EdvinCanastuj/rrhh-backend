using Microsoft.AspNetCore.Mvc;
using rrhh_backend.Data.DTOs;
using rrhh_backend.Data.Models;
using rrhh_backend.Services.Administracion;

namespace rrhh_backend.Controllers.Administracion
{
    [ApiController]
    [Route("api/permisos")]
    public class AdminPermisosController : Controller
    {
        private readonly AdminPermisosService _adminPermisosService;

        public AdminPermisosController(AdminPermisosService adminPermisosService)
        {
            _adminPermisosService = adminPermisosService;
        }

        [HttpGet("listar")]
        public async Task<ActionResult<List<AdminPermiso>>> GetAllPermisos()
        {
            try
            {
                var permisos = await _adminPermisosService.GetAllPermisos();
                return Ok(permisos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
        [HttpPost("rol/crear")]
        public async Task<IActionResult> CrearRolPermiso([FromBody] UserRolPermisoDto rolpermiso)
        {

            try
            {
                await _adminPermisosService.CrearRolesPermisos(rolpermiso.idRol, rolpermiso.idPermiso);
                return Ok(new { Message = "Permisos creado en base al rol correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = $"Error al intentar crear los permisos: {ex.Message}" }); ;
            }
        }
        [HttpPut("rol/actualizar/{idRole}")]
        public async Task<IActionResult> ActualizarRolPermisos(int idRole, [FromBody] int[] nuevosPermisos)
        {
            try
            {
                var existeRol = await _adminPermisosService.VerificarExistenciaRol(idRole);
                if (existeRol == false)
                {
                    return BadRequest(new { Error = "El rol no existe para poder actualizar los permisos." });
                }

                await _adminPermisosService.ActualizarRolPermisos(idRole, nuevosPermisos);

                return Ok(new { Message = "Roles y permisos actualizados correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = $"Error al intentar actualizar roles y permisos: {ex.Message}" });
            }
        }
        [HttpGet("rol/listar")]
        public async Task<IActionResult> ObtenerRolPermisos()
        {
            try
            {
                var rolPermisos = await _adminPermisosService.ObtenerRolesPermisos();
                return Ok(rolPermisos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = $"Error al intentar obtener roles y permisos: {ex.Message}" });
            }
        }
       
    }
}
