using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using rrhh_backend.Data;
using rrhh_backend.Data.DTOs;
using rrhh_backend.Data.Models;

namespace rrhh_backend.Services.Administracion
{
    public class AdminPermisosService
    {
        private readonly RrHhContext _context;

        public AdminPermisosService(RrHhContext context)
        {
            _context = context;
        }

        public async Task<List<AdminPermiso>> GetAllPermisos()
        {
            return await _context.AdminPermiso.ToListAsync();
        }

        public async Task CrearRolesPermisos(int idRol, int idPermiso)
        {
            try
            {
                var nuevoRolPermisos = new AdminRolesPermiso
                {
                    IdRole = idRol,
                    IdPermiso = idPermiso
                };
                _context.AdminRolesPermiso.Add(nuevoRolPermisos); ;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear Rol y permisos: {ex.Message}");
                throw;
            }
        }
        public async Task<List<string>> GetPermissionsByUserId(int userId)
        {
            try
            {
                var permissions = await _context.AdminUserRole
              .Where(uur => uur.IdUsuario == userId)
              .SelectMany(role => _context.AdminRolesPermiso.Where(urp => urp.IdRole == role.IdRole)) // Obtener la relación entre roles y permisos directamente
              .Select(urp => urp.IdPermisoNavigation.NombrePermiso) // Obtener el nombre del permiso
              .ToListAsync();

                return permissions;
            }
            catch (Exception ex)
            {
                // Manejar la excepción según tus necesidades
                Console.WriteLine($"Error al obtener los permisos del usuario: {ex.Message}");
                return new List<string>(); // Devolver una lista vacía en caso de error
            }
        }
        public async Task<List<ListRolPermisosDto>> ObtenerRolesPermisos()
        {
            try
            {
                var rolPermisos = await _context.AdminRolesPermiso
                    .Include(urp => urp.IdRoleNavigation)
                    .Include(urp => urp.IdPermisoNavigation)
                    .Select(urp => new
                    {
                        urp.IdRoleNavigation.IdRole,
                        urp.IdRoleNavigation.NombreRol,
                        urp.IdPermisoNavigation.IdPermiso,
                        urp.IdPermisoNavigation.NombrePermiso
                    })
                    .ToListAsync();
                var rolesPermisosAgrupados = rolPermisos
                    .GroupBy(rp => new { rp.IdRole, rp.NombreRol })
                    .Select(group => new ListRolPermisosDto
                    {
                        IdRole = group.Key.IdRole,
                        NombreRol = group.Key.NombreRol,
                        Permisos = group.Select(rp => new PermisoDto { IdPermiso = rp.IdPermiso, NombrePermiso = rp.NombrePermiso }).ToList()
                    })
                    .ToList();
                return rolesPermisosAgrupados;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener roles y permisos: {ex.Message}");
                throw;
            }
        }
        public async Task<List<UserRolesPermisosDto>> ObtenerRolesPermisosUsuario()
        {
            try
            {
                var rolesPermisos = await _context.AdminRolesPermiso
                    .Include(urp => urp.IdRoleNavigation)
                    .Include(urp => urp.IdPermisoNavigation)
                    .Select(urp => new
                    {
                        urp.IdRoleNavigation.IdRole,
                        urp.IdRoleNavigation.NombreRol,
                        urp.IdRoleNavigation.DescripcionRol,
                        urp.IdPermisoNavigation.IdPermiso,
                        urp.IdPermisoNavigation.NombrePermiso
                    })
                    .ToListAsync();

                var rolesPermisosAgrupados = rolesPermisos
                    .GroupBy(rp => new { rp.IdRole, rp.NombreRol, rp.DescripcionRol })
                    .Select(group => new UserRolesPermisosDto
                    {
                        IdRole = group.Key.IdRole,
                        NombreRol = group.Key.NombreRol,
                        DescripcionRol = group.Key.DescripcionRol,
                        Permisos = group.Select(rp => new PermisoDto { IdPermiso = rp.IdPermiso, NombrePermiso = rp.NombrePermiso }).ToList()
                    })
                    .ToList();

                return rolesPermisosAgrupados;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener roles y permisos: {ex.Message}");
                throw;
            }
        }


        public async Task ActualizarRolPermisos(int idRole, int[] nuevosPermisos)
        {
            try
            {
                // Eliminar los permisos existentes asociados al rol
                var permisosRol = await _context.AdminRolesPermiso
                    .Where(urp => urp.IdRole == idRole)
                    .ToListAsync();

                _context.AdminRolesPermiso.RemoveRange(permisosRol);

                // Agregar los nuevos permisos asociados al rol
                foreach (var idPermiso in nuevosPermisos)
                {
                    var nuevoRolPermiso = new AdminRolesPermiso
                    {
                        IdRole = idRole,
                        IdPermiso = idPermiso
                    };
                    _context.AdminRolesPermiso.Add(nuevoRolPermiso);
                }

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar los roles y permisos: {ex.Message}");
                throw;
            }
        }
        public async Task<bool?> VerificarExistenciaRol(int idRol)
        {
            try
            {
                var rolExistente = await _context.AdminRole.FindAsync(idRol);
                return rolExistente != null;
            }
            catch (Exception ex)
            {
                // En caso de error, devolvemos null en lugar de lanzar una excepción
                Console.WriteLine($"Error al verificar la existencia del rol: {ex.Message}");
                return null;
            }
        }
    }
}
