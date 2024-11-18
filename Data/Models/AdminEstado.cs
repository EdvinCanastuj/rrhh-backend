namespace rrhh_backend.Data.Models
{
    public class AdminEstado
    {
        public int IdEstado { get; set; }

        public string NombreEstado { get; set; } = null!;

        public string? Descripcion { get; set; }

        public virtual ICollection<AdminUser> UserUsers { get; set; } = new List<AdminUser>();
    }
}
