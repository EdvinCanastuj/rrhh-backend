using Microsoft.AspNetCore.Mvc;
using rrhh_backend.Data.DTOs;
using rrhh_backend.Services.Administracion;

namespace rrhh_backend.Controllers.Administracion
{
    [ApiController]
    [Route("api/UserRol")]
    public class AdminUserRolController : Controller
    {
        private readonly UserUserRolesService _userUserRolesService;
        private readonly AdminUserService _userService;

        public AdminUserRolController( UserUserRolesService userUserRolesService, AdminUserService adminUser)
        {
            _userUserRolesService = userUserRolesService;
            _userService = adminUser;
        }

        // POST: AsignarRol/AsignarRol
        [HttpPost("crear")]
        public async Task<IActionResult> AsignarRol([FromBody] AdminAsignarRolDto nuevoRol)
        {
            try
            {
                // Lógica para asignar un nuevo rol a un usuario
                await _userUserRolesService.CreaUserRol(nuevoRol.idUsuario, nuevoRol.idRol);

                // Puedes redirigir a la página de inicio de sesión u otra página después de asignar el rol
                return Ok(new { Message = "Rol asignado correctamente." });
            }
            catch (Exception ex)
            {
                // Manejar la excepción según tus necesidades
                return BadRequest(new { Error = $"Error al intentar asignar el rol: {ex.Message}" });
            }
        }
        // listar usuarios y roles
        [HttpGet("listar")]
        public async Task<ActionResult<List<AdminListUserRoleDto>>> GetAllUsersRoles()
        {
            try
            {
                var usersRoles = await _userUserRolesService.ObtenerUsersRoles();
                return Ok(usersRoles);
            }
            catch (Exception ex)
            {
                // Manejar la excepción según tus necesidades
                return BadRequest(new { Error = $"Error al intentar obtener los usuarios y roles: {ex.Message}" });
            }
        }
        // actualizar roles
        [HttpPut("actualizar/{idUsuario}")]
        public async Task<IActionResult> ActualizarUserRoles(int idUsuario, [FromBody] int[] nuevosRoles)
        {
            try
            {
                var existeUser = await _userService.ObtenerUserPorId(idUsuario);
                if (existeUser == null)
                {
                    return BadRequest(new { Error = "El usuario no existe para poder actualizar el rol o roles." });
                }

                await _userUserRolesService.ActualizarUserRoles(idUsuario, nuevosRoles);

                return Ok(new { Message = "Usuario y rol actualizados correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = $"Error al intentar actualizar usuario y rol: {ex.Message}" });
            }
        }

    }
}
