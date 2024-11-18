namespace rrhh_backend.Data.Models
{
    public class AdminPermiso
    {
        public int IdPermiso { get; set; }

        public string NombrePermiso { get; set; } = null!;

        public string? DescripcionPermiso { get; set; }

        public virtual ICollection<AdminRolesPermiso> UserRolesPermisos { get; set; } = new List<AdminRolesPermiso>();
    }
}
