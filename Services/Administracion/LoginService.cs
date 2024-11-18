using Org.BouncyCastle.Crypto.Fpe;
using rrhh_backend.Data;
using rrhh_backend.Data.Models;
using rrhh_backend.Data.DTOs;
using Microsoft.EntityFrameworkCore;

namespace rrhh_backend.Services.Administracion
{
    public class LoginService
    {
        private RrHhContext _context;
        private IEmailService _emailService;

        public LoginService( RrHhContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<AdminUser?> GetUser(AdminUserUserDto user)
        {
            var usuario = await _context.AdminUser
                .SingleOrDefaultAsync(x => x.Email == user.Email && x.Password == user.Password);

            if (usuario != null)
            {
                if (usuario.IdEstado == 1)
                {
                    return usuario;
                }
                else if (usuario.IdEstado == 2)
                {
                    throw new InvalidOperationException("Su usuario ya no está activo en el sistema.");
                }
                // Otros estados pueden manejarse según sea necesario
            }

            throw new InvalidOperationException("Nombre de usuario o contraseña incorrectos.");
        }



        public async Task<AdminUser?> FindByEmailAsync(string email)
        {
            return await _context.AdminUser
                .SingleOrDefaultAsync(x => x.Email == email);
        }
        public async Task<(string? latestToken, DateTime? latestCreationDate)> GetLatestTokenAndDateByUserIdAsync(int userId)
        {
            var latestTokenAndDate = await _context.AdminResetPassword
                .Where(x => x.IdUsuario == userId)
                .OrderByDescending(x => x.FechaExpiracionToken)
                .Select(x => new { Token = x.NombreToken, CreationDate = x.FechaExpiracionToken })
                .FirstOrDefaultAsync();

            return (latestTokenAndDate?.Token, latestTokenAndDate?.CreationDate);
        }


        public async Task SaveResetTokenAsync(int userId, string resetToken, DateTime expirationDate)
        {
            try
            {
                var tokenEntity = new AdminResetPassword
                {
                    NombreToken = resetToken,
                    FechaCreacionToken = DateTime.Now,
                    IdUsuario = userId,
                    FechaExpiracionToken = expirationDate
                };

                _context.AdminResetPassword.Add(tokenEntity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar guardar el token de restablecimiento de contraseña.", ex);
            }
        }


        // En tu servicio LoginService.cs
        public async Task UpdateUser(AdminUser user)
        {
            // Verificar si el usuario existe en la base de datos
            var existingUser = await _context.AdminUser.FirstOrDefaultAsync(u => u.IdUsuario == user.IdUsuario);

            if (existingUser != null)
            {
                // Actualizar las propiedades necesarias del usuario
                existingUser.Password = user.Password; // O cualquier otra propiedad que necesites actualizar

                // Guardar los cambios en la base de datos
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    // Loguea o imprime la excepción para ver el mensaje de error
                    Console.WriteLine(ex.Message);
                }

            }
            else
            {
                // Manejar el caso en que el usuario no existe (si es necesario)
                throw new Exception("Usuario no encontrado.");
            }
        }

        public async Task<string?> GetEmailFromTokenAsync(string resetToken)
        {
            try
            {
                // Buscar el token en la tabla UserResetPassword
                var userResetPassword = await _context.AdminResetPassword
                    .FirstOrDefaultAsync(x => x.NombreToken == resetToken);

                if (userResetPassword != null)
                {
                    // Si se encuentra el token, obtener el ID de usuario asociado
                    var userId = userResetPassword.IdUsuario;

                    // Buscar el usuario en la tabla UserUser usando el ID obtenido
                    var userUser = await _context.AdminUser
                        .FirstOrDefaultAsync(x => x.IdUsuario == userId);

                    // Si se encuentra el usuario, devolver su correo electrónico
                    return userUser?.Email;
                }

                // Si el token no se encuentra en la tabla UserResetPassword, devolver null
                return null;
            }
            catch (Exception ex)
            {
                // Manejar la excepción según tus necesidades (puedes registrarla, lanzarla, etc.)
                Console.WriteLine($"Error al obtener el correo electrónico desde el token: {ex.Message}");
                return null;
            }
        }

        public async Task RegistrarAccionEnBitacora(string accion, int Idusuario)
        {
            try
            {
                var nuevaBitacora = new AdminBitacoraUsuario
                {
                    AccionBitacora = accion,
                    FechaBitacora = DateTime.Now,
                    IdUsuario = Idusuario
                };

                _context.AdminBitacoraUsuarios.Add(nuevaBitacora);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar registrar la acción en la bitácora.", ex);
            }
        }
    }
}
