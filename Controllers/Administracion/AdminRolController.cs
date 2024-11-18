using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using rrhh_backend.Data.DTOs;
using rrhh_backend.Data.Models;
using rrhh_backend.Services.Administracion;

namespace rrhh_backend.Controllers.Administracion
{
    [ApiController]
    [Route("api/rol")]
    public class AdminRolController : Controller
    {
        private readonly AdminUserRolService _adminUserRolesService;
        private readonly AdminUserService _userUserService;

        public AdminRolController(AdminUserRolService adminUserRolesService, AdminUserService userUserService)
        {
            _adminUserRolesService = adminUserRolesService;
            _userUserService = userUserService;
        }
        // [Authorize(Roles = ("CrearRol"))]
        [HttpPost("crear")]
        public async Task<IActionResult> CrearRol([FromBody] AdminUserRoleDto nuevoRol)
        {

            try
            {
                await _adminUserRolesService.CrearRol(nuevoRol.nombreRol, nuevoRol.descripcionRol);
                return Ok(new { Message = "Rol creado correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = $"Error al intentar crear el rol: {ex.Message}" }); ;
            }
        }
        //   [Authorize(Roles = ("CrearRol, AsignarPermisos, AsignarRol"))]
        [HttpGet("listar/{userId}")]
        public async Task<ActionResult<List<AdminRole>>> GetAllRol(int userId)
        {
            try
            {
                var userExiste = await _userUserService.ObtenerUserPorId(userId);
                if (userExiste == null)
                {
                    return BadRequest(new { Error = $"El usuario no existe para obtener los roles no asignados." });
                }
                var roles = await _adminUserRolesService.GetRolesNotAssignedToUser(userId);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // [Authorize(Roles = ("CrearRol, AsignarPermisos, AsignarRol"))]
        [HttpGet("listar")]
        public async Task<ActionResult<List<AdminRolListDto>>> GetAllRoles()
        {
            try
            {
                var roles = await _adminUserRolesService.GetAllRole();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener todos los roles: {ex.Message}");
            }
        }

        //GetRolSinPermisoAsociados
        [HttpGet("listarSinPermisos")]
        public async Task<ActionResult<List<AdminRolListDto>>> GetRolSinPermisoAsociados()
        {
            try
            {
                var roles = await _adminUserRolesService.GetRolSinPermisoAsociados();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener todos los roles: {ex.Message}");
            }
        }


        //actualizar rol
        //[Authorize(Roles = ("CrearRol"))]
        [HttpPut("actualizar/{idRol}")]
        public async Task<ActionResult> ActualizarRol(int idRol, [FromBody] AdminUserRoleDto rolDTO)
        {
            try
            {
                var userExiste = await _adminUserRolesService.VerificarExistenciaRol(idRol);
                if (userExiste == false)
                {
                    return BadRequest(new { Error = $"El usuario no existe para obtener los roles no asignados." });
                }
                await _adminUserRolesService.ActualizarRol(idRol, rolDTO.nombreRol, rolDTO.descripcionRol);
                return Ok("Rol actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el rol: {ex.Message}");
            }
        }

    }
}
