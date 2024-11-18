using rrhh_backend.Data.Models;

namespace rrhh_backend.Data.Models
{
    public class AdminUser
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public int? IdEstado { get; set; }
        public int? IdColaborador { get; set; }
        public virtual RHColaborador? IdColaboradorNavigation { get; set; }
        public virtual AdminEstado? IdEstadoNavigation { get; set; }
        public virtual ICollection<AdminBitacoraUsuario> UserBitacoraUsuarios { get; set; } = new List<AdminBitacoraUsuario>();
        public virtual ICollection<AdminResetPassword> UserResetPasswords { get; set; } = new List<AdminResetPassword>();
        public virtual ICollection<AdminUserRole> UserUserRoles { get; set; } = new List<AdminUserRole>();

    }
}
