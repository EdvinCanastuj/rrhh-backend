namespace rrhh_backend.Data.Models
{
    public class AdminRole
    {
        public int IdRole { get; set; }

        public string NombreRol { get; set; } = null!;

        public string? DescripcionRol { get; set; }

        public virtual ICollection<AdminRolesPermiso> UserRolesPermisos { get; set; } = new List<AdminRolesPermiso>();

        public virtual ICollection<AdminUserRole> UserUserRoles { get; set; } = new List<AdminUserRole>();
    }
}
