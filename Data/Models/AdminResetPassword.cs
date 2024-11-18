namespace rrhh_backend.Data.Models
{
    public class AdminResetPassword
    {
        public int IdToken { get; set; }

        public string NombreToken { get; set; } = null!;

        public DateTime FechaCreacionToken { get; set; }

        public DateTime FechaExpiracionToken { get; set; }

        public int? IdUsuario { get; set; }
        public virtual AdminUser? IdUsuarioNavigation { get; set; }

    }
}
