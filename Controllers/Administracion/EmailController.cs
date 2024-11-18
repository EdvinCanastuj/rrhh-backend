using Microsoft.AspNetCore.Mvc;
using rrhh_backend.Data.DTOs;
using rrhh_backend.Services.Administracion;
using System.Security.Cryptography;

namespace rrhh_backend.Controllers.Administracion
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly LoginService loginService;
        private IConfiguration config;
        public EmailController(IEmailService emailService, LoginService loginService, IConfiguration config)
        {
            this.loginService = loginService;
            _emailService = emailService;
            this.config = config;
        }


        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequesModelDto model)
        {
            try
            {
                await _emailService.SendEmailAsync(model.To, model.Subject, model.Body);
                return Ok(new { Message = "Correo electrónico enviado correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = $"Error al enviar el correo electrónico: {ex.Message}" });
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto model)
        {
            try
            {
                // Verifica si el correo electrónico existe en tu base de datos
                var user = await loginService.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    //return NotFound(new { Message = "Usuario no encontrado." });
                    return BadRequest(new { message = "Correo no encontrado" });
                }

                // Genera un token seguro de longitud 25 caracteres
                var resetToken = GenerateSecureToken(50);

                // Almacena el token en tu base de datos junto con el usuario y su fecha de expiración
                var expirationDate = DateTime.Now.AddMinutes(5); // Token válido por 24 horas
                await loginService.SaveResetTokenAsync(user.IdUsuario, resetToken, expirationDate);


                // Envía un correo electrónico al usuario con el enlace para restablecer la contraseña
                //var resetLink = $"http://localhost:3000/auth/new-password/{resetToken}";
                var resetLink = $"https://pyrho-73828.web.app/auth/new-password/{resetToken}";
                var subject = "Restablecimiento de Contraseña";
                var body = $"Se va a restablecer la contraseña del usuario {user.NombreUsuario}. Por favor, haz clic en el siguiente enlace para establecer tu nueva contraseña: {resetLink}";

                await _emailService.SendEmailAsync(model.Email, subject, body);

                return Ok(new { Message = "Correo electrónico de restablecimiento de contraseña enviado correctamente." });

            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = $"Error al intentar restablecer la contraseña: {ex.Message}" });
            }
        }

        private string GenerateSecureToken(int length)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var tokenBytes = new byte[length];
                rng.GetBytes(tokenBytes);

                // Convertir los bytes a una cadena hexadecimal
                string token = BitConverter.ToString(tokenBytes).Replace("-", "");

                return token;
            }
        }
    }
}
