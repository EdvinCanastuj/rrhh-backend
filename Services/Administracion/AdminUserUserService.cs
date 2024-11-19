using Microsoft.EntityFrameworkCore;
using rrhh_backend.Data;
using rrhh_backend.Data.DTOs;
using rrhh_backend.Data.Models;

namespace rrhh_backend.Services.Administracion
{
    public class AdminUserUserService
    {
        private readonly RrHhContext _context;

        public AdminUserUserService(RrHhContext context)
        {
            _context = context;
        }
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
        //obtener usuarios con rol asignado
        public async Task<List<AdminRoleUsuarioDto>> GetUsuariosConRoles()
        {
            // Obtenemos todos los usuarios que tienen roles asignados
            return await _context.AdminUserRole
                .Select(ur => new AdminRoleUsuarioDto
                {
                    IdRole = ur.IdRole,
                    NombreRol = ur.IdRoleNavigation.NombreRol,
                    IdUsuario = ur.IdUsuario,
                    Email = ur.IdUsuarioNavigation.Email,
                    NombreUsuario = ur.IdUsuarioNavigation.NombreUsuario,
                    IdColaborador = ur.IdUsuarioNavigation.IdColaborador ?? 0, // Asignar 0 si es null
                    Nombres = ur.IdUsuarioNavigation.IdColaboradorNavigation != null ? ur.IdUsuarioNavigation.IdColaboradorNavigation.Nombres : "",
                    PrimerApellido = ur.IdUsuarioNavigation.IdColaboradorNavigation != null ? ur.IdUsuarioNavigation.IdColaboradorNavigation.PrimerApellido : "",
                    SegundoApellido = ur.IdUsuarioNavigation.IdColaboradorNavigation != null ? ur.IdUsuarioNavigation.IdColaboradorNavigation.SegundoApellido : ""
                })
                .ToListAsync();
        }

        //Insertamos nuevo usuario
        public async Task CrearUsuario(string email, string password, string nombreUsuario, int? idColaborador)
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
        public async Task AsignarRolUsuario(int idUsuario, int idRol)
        {
            try
            {
                // Verificar si el usuario ya tiene el rol asignado
                var existeRolUsuario = await _context.AdminUserRole
                    .AnyAsync(ur => ur.IdUsuario == idUsuario && ur.IdRole == idRol);
                if (existeRolUsuario)
                {
                    throw new InvalidOperationException("El usuario ya tiene el rol asignado.");
                }

                // Asignar el rol al usuario
                var nuevoRolUsuario = new AdminUserRole
                {
                    IdUsuario = idUsuario,
                    IdRole = idRol,
                    FechaAsignacion = DateTime.Now
                };
                _context.AdminUserRole.Add(nuevoRolUsuario);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Dependiendo de los requisitos de tu aplicación.
                Console.WriteLine($"Error al asignar rol al usuario: {ex.Message}");
                throw;
            }
        }
    }
}
