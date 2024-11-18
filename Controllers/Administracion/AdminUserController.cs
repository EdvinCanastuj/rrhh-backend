using Microsoft.AspNetCore.Mvc;
using rrhh_backend.Services.Administracion;
using rrhh_backend.Data.DTOs;
using System.Text;
using System.Security.Cryptography;

namespace rrhh_backend.Controllers.Administracion
{
    [ApiController]
    [Route("api/UserUser")]
    public class AdminUserController : Controller
    {

        private readonly AdminUserUserService _userUserService;

        public AdminUserController(AdminUserUserService userUserService)
        {
            _userUserService = userUserService;
        }

        // POST: NuevoUsuario/CrearUsuario
        //[Authorize(Roles = ("CrearUsuario"))]
        [HttpPost("crear")]
        public async Task<IActionResult> CrearUsuario([FromBody] AdminNewUserDto nuevoUsuario)

        {
            try
            {
                // Lógica para crear un nuevo usuario
                await _userUserService.CrearUsuario(nuevoUsuario.email, nuevoUsuario.password, nuevoUsuario.nombreUsuario, nuevoUsuario.idColaborador);

                // Puedes redirigir a la página de inicio de sesión u otra página después de crear el usuario
                return Ok(new { Message = "Usuario creado correctamente." });
            }
            catch (Exception ex)
            {
                // Manejar la excepción según tus necesidades
                return BadRequest(new { Error = $"Error al intentar crear el usuario: {ex.Message}" });
            }

        }

        // Método para hashear la contraseña (puedes utilizar la implementación que prefieras)
        private string HashPassword(string password)
        {
            // Aquí deberías usar un algoritmo de hash seguro, como bcrypt      
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }



        //Obtener todos los usuarios  de la tabala User_User
        // [Authorize(Roles = ("CrearUsuario, AsignarRol"))]
        [HttpGet("listar")]
        public async Task<ActionResult<List<AdminUserListDto>>> GetAllUsers()
        {
            try
            {
                var users = await _userUserService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        //GetUsuariosSinRoles
        [HttpGet("listarUsuariosSinRoles")]
        public async Task<ActionResult<List<AdminUserListDto>>> GetUsuariosSinRoles()
        {
            try
            {
                var users = await _userUserService.GetUsuariosSinRoles();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: User_User/ActualizarUsuario
        // [Authorize(Roles = "CrearUsuario")]
        [HttpPut("actualizar/{id}")]
        public async Task<IActionResult> ActualizarUsuario(int id, [FromBody] AdminUpdateUserDto datosUsuario)
        {
            try
            {
                var userExistente = await _userUserService.ObtenerUserPorId(id);
                if (userExistente == null)
                {
                    return BadRequest(new { Error = $"El usuario no existe para poder actualizarlo." });
                }
                // Lógica para actualizar un usuario existente
                await _userUserService.ActualizarUsuario(id, datosUsuario.nombreUsuario, datosUsuario.email, datosUsuario.idColaborador);

                return Ok(new { Message = "Usuario actualizado correctamente." });
            }
            catch (Exception ex)
            {
                // Manejar la excepción según tus necesidades
                return BadRequest(new { Error = $"Error al intentar actualizar el usuario: {ex.Message}" });
            }
        }

        [HttpPost("asingarRolUsuario")]
        public async Task<IActionResult> AsignarRolUsuario([FromBody] AdminAsignarRolDto asignarRol)
        {
            try
            {
                var userExistente = await _userUserService.ObtenerUserPorId(asignarRol.idUsuario);
                if (userExistente == null)
                {
                    return BadRequest(new { Error = $"El usuario no existe para poder asignarle un rol." });
                }
                await _userUserService.AsignarRolUsuario(asignarRol.idUsuario, asignarRol.idRol);
                return Ok(new { Message = "Rol asignado correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = $"Error al intentar asignar el rol al usuario: {ex.Message}" });
            }
        }   

    }
}
