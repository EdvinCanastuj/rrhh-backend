using Microsoft.EntityFrameworkCore;
using rrhh_backend.Data;
using rrhh_backend.Data.DTOs;
using rrhh_backend.Data.Models;

namespace rrhh_backend.Services.Administracion
{
    public class AdminUserService
    {
        private readonly RrHhContext _context;
        public AdminUserService(RrHhContext context)
        {
            _context = context;

        }

        //obtenemos todos los usuarios de la tabla User_User
        public async Task<List<AdminUserListDto>> GetAllUsers()
        {
            return await _context.AdminUser
                .Include(u => u.IdEstadoNavigation)
                .Include(u => u.IdColaboradorNavigation)
                .Select(u => new AdminUserListDto
                {
                    IdUsuario = u.IdUsuario,
                    NombreUsuario = u.NombreUsuario,
                    Email = u.Email,
                    FechaCreacion = u.FechaCreacion,
                    IdEstado = u.IdEstado, // Incluir IdEstado si es necesario
                    NombreEstado = u.IdEstadoNavigation.NombreEstado,
                    IdColaborador = u.IdColaboradorNavigation != null ? u.IdColaboradorNavigation.IdColaborador : (int?)null,
                    Nombres = u.IdColaboradorNavigation != null ? u.IdColaboradorNavigation.Nombres : null,
                    PrimerApellido = u.IdColaboradorNavigation != null ? u.IdColaboradorNavigation.PrimerApellido : null,
                    SegundoApellido = u.IdColaboradorNavigation != null ? u.IdColaboradorNavigation.SegundoApellido : null,

                })
                .ToListAsync();
        }

        //obtener usuarios que no tenga rol asignado
        public async Task<List<AdminUser>> GetUsuariosSinRoles()
        {
            //obtenemos todos los ususarios
            var usuarios = await _context.AdminUser.ToListAsync();

            //Filtrar los usuarios que no tienen roles
            var usuariosSinRoles = usuarios.Where(user =>
                !_context.AdminUserRole.Any(rolUsuario =>
                   rolUsuario.IdUsuario == user.IdUsuario)
            ).ToList();

            return usuariosSinRoles;
        }

        //Insertamos nuevo usuario
        public async Task CrearUsuario(string nombreUsuario, string password, string email, int? idColaborador)
        {
            try
            {
                // Verificar si el nombre de usuario ya existe
                var existingUsername = await _context.AdminUser.FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);
                if (existingUsername != null)
                {
                    throw new InvalidOperationException("El nombre de usuario ya está en uso.");
                }

                // Verificar si el correo electrónico ya existe
                var existingEmail = await _context.AdminUser.FirstOrDefaultAsync(u => u.Email == email);
                if (existingEmail != null)
                {
                    throw new InvalidOperationException("El correo electrónico ya está registrado.");
                }

                // Si el nombre de usuario y el correo electrónico no existen, crear un nuevo usuario
                var nuevoUsuario = new AdminUser
                {
                    NombreUsuario = nombreUsuario,
                    Password = password,
                    Email = email,
                    FechaCreacion = DateTime.Now,
                    IdEstado = 1,
                    IdColaborador = idColaborador
                };
                _context.AdminUser.Add(nuevoUsuario);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Dependiendo de los requisitos de tu aplicación, puedes manejar la excepción aquí.
                Console.WriteLine($"Error al crear usuario: {ex.Message}");
                throw;
            }
        }

        //OBTENEMOS USUARIOS POR ID
        public async Task<AdminUser> ObtenerUserPorId(int id)
        {
            return await _context.AdminUser.FindAsync(id);
        }

        //actualizar solamente nombreUsuario y email

        //public async Task ActualizarUsuario(int userId, string nuevoNombreUsuario, string nuevoEmail)
        //{
        //    try
        //    {
        //        var usuario = await _context.UserUsers.FindAsync(userId);

        //        if (usuario != null)
        //        {
        //            usuario.NombreUsuario = nuevoNombreUsuario;
        //            usuario.Email = nuevoEmail;

        //            await _context.SaveChangesAsync();
        //        }
        //        else
        //        {
        //            Console.WriteLine($"No se encontró el usuario con ID {userId}");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Dependiendo de los requisitos de tu aplicación.
        //        Console.WriteLine($"Error al actualizar usuario: {ex.Message}");
        //        throw;
        //    }
        //}

        public async Task ActualizarUsuario(int userId, string nuevoNombreUsuario, string nuevoEmail, int? idColaborador)
        {
            try
            {
                // Verificar si el nuevo nombre de usuario ya existe
                var existingUsername = await _context.AdminUser.FirstOrDefaultAsync(u => u.NombreUsuario == nuevoNombreUsuario && u.IdUsuario != userId);
                if (existingUsername != null)
                {
                    throw new InvalidOperationException("El nuevo nombre de usuario ya está en uso.");
                }

                // Verificar si el nuevo correo electrónico ya existe
                var existingEmail = await _context.AdminUser.FirstOrDefaultAsync(u => u.Email == nuevoEmail && u.IdUsuario != userId);
                if (existingEmail != null)
                {
                    throw new InvalidOperationException("El nuevo correo electrónico ya está registrado.");
                }

                // Actualizar el usuario si existe
                var usuario = await _context.AdminUser.FindAsync(userId);
                if (usuario != null)
                {
                    usuario.NombreUsuario = nuevoNombreUsuario;
                    usuario.Email = nuevoEmail;
                    usuario.IdColaborador = idColaborador;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    Console.WriteLine($"No se encontró el usuario con ID {userId}");
                }
            }
            catch (Exception ex)
            {
                // Dependiendo de los requisitos de tu aplicación.
                Console.WriteLine($"Error al actualizar usuario: {ex.Message}");
                throw;
            }
        }
    }
}
