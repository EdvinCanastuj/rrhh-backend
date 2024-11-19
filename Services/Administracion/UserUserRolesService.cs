using Microsoft.EntityFrameworkCore;
using rrhh_backend.Data;
using rrhh_backend.Data.DTOs;
using rrhh_backend.Data.Models;

namespace rrhh_backend.Services.Administracion
{
    public class UserUserRolesService
    {
        private readonly RrHhContext _context;

        public UserUserRolesService(RrHhContext context)
        {
            _context = context;
        }


        public async Task<List<string>> GetRolesByUserIdAsync(int userId)
        {
            try
            {
                // Realizar una consulta para obtener los roles asociados al usuario
                var roles = await _context.AdminUserRole
                                          .Where(uur => uur.IdUsuario == userId)
                                          .Select(uur => uur.IdRoleNavigation.NombreRol)
                                          .ToListAsync();

                return roles;
            }
            catch (Exception ex)
            {
                // Manejar la excepción según tus necesidades
                Console.WriteLine($"Error al obtener los roles del usuario: {ex.Message}");
                return new List<string>(); // Devolver una lista vacía en caso de error
            }
        }

        //Insertamos Roles en base al usuario
        public async Task CreaUserRol(int idUsuario, int idRole)
        {
            try
            {
                var userRol = new AdminUserRole
                {
                    IdUsuario = idUsuario,
                    IdRole = idRole,
                    FechaAsignacion = DateTime.Now
                };
                _context.AdminUserRole.Add(userRol);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al asignar roles al usuario : {ex.Message}");
                throw;
            }
        }

        //listado usuario y roles
        public async Task<List<AdminListUserRoleDto>> ObtenerUsersRoles()
        {
            try
            {
                var userRoles = await _context.AdminUserRole
                    .Include(urp => urp.IdUsuarioNavigation)
                    .Include(urp => urp.IdRoleNavigation)
                    .Select(urp => new
                    {
                        urp.IdUsuarioNavigation.IdUsuario,
                        urp.IdUsuarioNavigation.Email,
                        urp.IdUsuarioNavigation.NombreUsuario,
                        urp.IdRoleNavigation.IdRole,
                        urp.IdRoleNavigation.NombreRol
                    })
                    .ToListAsync();

                var userRolesAgrupados = userRoles
                    .GroupBy(rp => new { rp.IdUsuario, rp.Email })
                    .Select(group => new AdminListUserRoleDto
                    {
                        IdUsuario = group.Key.IdUsuario,
                        Email = group.Key.Email,
                        NombreUsuario = group.Select(rp => rp.NombreUsuario).FirstOrDefault(),
                        RolList = group.Select(rp => new AdminRolListDto { IdRole = rp.IdRole, NombreRol = rp.NombreRol }).ToList()
                    })
                    .ToList();

                return userRolesAgrupados;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener usuario y roles: {ex.Message}");
                throw;
            }
        }

        //actualizar
        public async Task ActualizarUserRoles(int idUsuario, int[] nuevosRoles)
        {

            try
            {
                var userRoles = await _context.AdminUserRole
                    .Where(urp => urp.IdUsuario == idUsuario)
                    .ToListAsync();

                _context.AdminUserRole.RemoveRange(userRoles);

                foreach (var idRole in nuevosRoles)
                {
                    var nuevoRolUser = new AdminUserRole
                    {
                        IdUsuario = idUsuario,
                        FechaAsignacion = DateTime.Now,
                        IdRole = idRole
                    };
                    _context.AdminUserRole.Add(nuevoRolUser);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar los usuario y roles: {ex.Message}");
                throw;
            }
        }


    }

}
