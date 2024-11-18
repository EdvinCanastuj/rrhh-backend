namespace rrhh_backend.Data.Models
{
    public class AdminRolesPermiso
    {
        public int IdRolePermiso { get; set; }

        public int IdRole { get; set; }

        public int IdPermiso { get; set; }

        public virtual AdminPermiso IdPermisoNavigation { get; set; } = null!;

        public virtual AdminRole IdRoleNavigation { get; set; } = null!;
    }
}
