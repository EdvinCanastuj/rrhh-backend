using Microsoft.EntityFrameworkCore;
using rrhh_backend.Data;
using rrhh_backend.Data.Models;

namespace rrhh_backend.Services.Administracion
{
    public class AdminUserRolService
    {
        private readonly RrHhContext _context;

        public AdminUserRolService(RrHhContext context)
        {
            _context = context;
        }
        //obtenemos todos los Roles de los usuarios 
        public async Task<List<AdminRole>> GetAllRole()
        {
            return await _context.AdminRole.ToListAsync();
        }


        public async Task<List<AdminRole>> GetRolSinPermisoAsociados()
        {
            //obtenemos todos los roles
            var roles = await _context.AdminRole.ToListAsync();


            //Filtrar los roles que no tiene permidos asociados
            var rolesSinPermisos = roles.Where(rol =>
            !_context.AdminRolesPermiso.Any(permisoRol =>
                permisoRol.IdRole == rol.IdRole)
            ).ToList();

            return rolesSinPermisos;
        }

        //roles que no tiene asignado el usuario 
        public async Task<List<AdminRole>> GetRolesNotAssignedToUser(int userId)
        {
            // Obtener los roles asignados al usuario
            var rolesAsignados = await _context.AdminUserRole
                .Where(uur => uur.IdUsuario == userId)
                .Select(uur => uur.IdRole)
                .ToListAsync();

            // Obtener los roles que el usuario no tiene asignados
            var rolesNoAsignados = await _context.AdminRole
                .Where(ur => !rolesAsignados.Contains(ur.IdRole))
                .ToListAsync();

            return rolesNoAsignados;
        }



        //insertamos un nuevo rol
        public async Task CrearRol(string nombreRol, string descripcionRol)
        {
            try
            {
                var nuevoRol = new AdminRole
                {
                    NombreRol = nombreRol,
                    DescripcionRol = descripcionRol,
                    // IdModulos = idModulos
                };
                _context.AdminRole.Add(nuevoRol);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear Rol : {ex.Message}");
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



        //actualizar el rol
        public async Task ActualizarRol(int idRol, string nuevoNombreRol, string nuevaDescripcionRol)
        {
            try
            {
                var rolExistente = await _context.AdminRole.FindAsync(idRol);
                if (rolExistente != null)
                {
                    rolExistente.NombreRol = nuevoNombreRol;
                    rolExistente.DescripcionRol = nuevaDescripcionRol;
                    //  rolExistente.IdModulos = nuevoIdModulo;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception($"No se encontró el rol con ID: {idRol}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el rol: {ex.Message}");
            }
        }
    }
}
