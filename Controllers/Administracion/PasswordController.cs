using Microsoft.AspNetCore.Mvc;
using rrhh_backend.Data.DTOs;
using rrhh_backend.Services.Administracion;
using System.Security.Cryptography;
using System.Text;

namespace rrhh_backend.Controllers.Administracion
{

    [ApiController]
    [Route("api/[controller]")]
    public class PasswordController : Controller
    {
        private readonly LoginService loginService;
        private readonly IConfiguration config;
        public PasswordController(LoginService loginService, IConfiguration config)
        {
            this.loginService = loginService;
            this.config = config;
        }

        [HttpPost("reset")]
        public async Task<IActionResult> NewPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            try
            {
                var email = await loginService.GetEmailFromTokenAsync(resetPasswordDto.ResetToken);


                var user = await loginService.FindByEmailAsync(email);

                if (user == null)
                {
                    return NotFound(new { message = "Usuario no encontrado." });
                }
                // Verificar que el token recibido coincida con el almacenado en el usuario
                var tokenData = await loginService.GetLatestTokenAndDateByUserIdAsync(user.IdUsuario);
                if (tokenData.latestToken == resetPasswordDto.ResetToken && tokenData.latestCreationDate > DateTime.Now)
                {

                    // Actualizar la contraseña del usuario
                    user.Password = resetPasswordDto.NewPassword;
                    await loginService.UpdateUser(user);

                    await loginService.RegistrarAccionEnBitacora("Cambio contraseña", user.IdUsuario);

                    return Ok(new { Message = "Contraseña actualizada correctamente." });
                }
                else
                {

                    return BadRequest(new { message = "Token inválido." });
                }




            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = $"Error al intentar actualizar la contraseña: {ex.Message}" });
            }
        }

        // Método para hashear la contraseña (puedes utilizar la implementación que prefieras)
        private string HashPassword(string password)
        {
            // Aquí deberías usar un algoritmo de hash seguro, como bcrypt
            // Este es solo un ejemplo básico, no lo uses en producción
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
    
}
