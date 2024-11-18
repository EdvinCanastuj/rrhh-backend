namespace rrhh_backend.Data.Models
{
    public class AdminUserRole
    {
        public int IdUserRoles { get; set; }

        public int IdUsuario { get; set; }

        public int IdRole { get; set; }

        public DateTime FechaAsignacion { get; set; }

        public virtual AdminRole IdRoleNavigation { get; set; } = null!;

        public virtual AdminUser IdUsuarioNavigation { get; set; } = null!;
    }
}
