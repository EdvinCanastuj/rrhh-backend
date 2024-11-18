using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace rrhh_backend.Controllers.Administracion
{
    [Authorize] // Requiere autenticación para acceder a este controlador
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        [HttpGet("my-account")]
        public IActionResult GetMyAccount()
        {
            try

            {
                // Obtén la información del usuario actual desde el contexto de identidad
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userName = User.FindFirst(ClaimTypes.Name)?.Value;

                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userName))
                {
                    // El token de autorización no es válido
                    return Unauthorized(new { message = "Token de autorización no válido" });
                }

                // Puedes devolver cualquier información adicional del usuario según tus necesidades
                var user = new { UserId = userId, UserName = userName, Email = "usuario@example.com" };

                return Ok(new { user });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }
    }
}
