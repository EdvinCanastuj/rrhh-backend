using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer.Server;
using rrhh_backend.Data.DTOs;
using rrhh_backend.Data.Models;
using rrhh_backend.Services.Administracion;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Google.Apis.Auth;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace rrhh_backend.Controllers.Administracion
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly LoginService loginService;
        private IConfiguration config;
        private readonly UserUserRolesService _userRoles;
        private readonly UserPermisosService permisos;
        public LoginController(LoginService loginService, IConfiguration config, UserPermisosService permisos, UserUserRolesService userRoles)
        {
            this.loginService = loginService;
            this.config = config;
            this.permisos = permisos;
            _userRoles = userRoles;
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> Login(AdminUserUserDto userDto)
        {
            try
            {
                var user = await loginService.GetUser(userDto);

                if (user is null)
                {
                    return BadRequest(new { message = "Credenciales inválidas" });
                }

                // Generar un token
                string jwtToken = await GenerateToken(user);

                await loginService.RegistrarAccionEnBitacora("Inicio al sistema", user.IdUsuario);

                return Ok(new { accessToken = jwtToken, user });
            }
            catch (InvalidOperationException ex)
            {
                // Manejar la excepción si el usuario no está activo
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Manejar otras excepciones
                return StatusCode(500, new { message = $"Ocurrió un error al procesar la solicitud. {ex.Message}" });
            }
        }
        [HttpPost("google")]
        public async Task<IActionResult> GoogleLogin( TokenDto tokenDto)
        {
            try
            {
                Console.WriteLine($"Token recibido: {tokenDto.Token}");

                var payload = await GoogleJsonWebSignature.ValidateAsync(tokenDto.Token);

                // Datos del usuario de Google
                var googleId = payload.Subject;
                var email = payload.Email;
                var name = payload.Name;

                var user = await loginService.FindByEmailAsync(email);
                if (user is null)
                {
                    return BadRequest(new { message = "Credenciales inválidas" });
                }
                // Generar un token
                string jwtToken = await GenerateToken(user);

                await loginService.RegistrarAccionEnBitacora("Inicio al sistema", user.IdUsuario);

                return Ok(new { accessToken = jwtToken, user });
            }
            catch (InvalidOperationException ex)
            {
                // Manejar la excepción si el usuario no está activo
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Manejar otras excepciones
                return StatusCode(500, new { message = $"Ocurrió un error al procesar la solicitud. {ex.Message}" });
            }

        }



        private async Task<string> GenerateToken(AdminUser user)
        {
            // Obtener los roles del usuario
            //List<string> roles = await _userRoles.GetRolesByUserIdAsync(user.IdUsuario);
            List<string> roles = await permisos.GetPermissionsByUserId(user.IdUsuario);


            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.NombreUsuario),
                    new Claim(ClaimTypes.NameIdentifier, user.IdUsuario.ToString())
                };

            // Agregar los roles como reclamaciones
            foreach (var roleItem in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, roleItem));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("Jwt:SecretKey").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken
            (
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: creds
            );

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }

    }
}
