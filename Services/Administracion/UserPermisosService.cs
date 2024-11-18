using Microsoft.EntityFrameworkCore;
using rrhh_backend.Data;
using rrhh_backend.Data.Models;

namespace rrhh_backend.Services.Administracion
{
    public class UserPermisosService
    {
        private readonly RrHhContext _context;

        public UserPermisosService(RrHhContext context)
        {
            _context = context;
        }

        //Obtener todos los permisos
        public async Task<List<AdminPermiso>> GetPermisos()
        {
            return await _context.AdminPermiso.ToListAsync();
        }

        //Obtener permisos por Usuario
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

    }

}
