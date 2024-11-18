namespace rrhh_backend.Data.Models
{
    public class AdminBitacoraUsuario
    {
        public int IdBitacoraUser { get; set; }

        public string AccionBitacora { get; set; } = null!;

        public DateTime FechaBitacora { get; set; }

        public int? IdUsuario { get; set; }

        public virtual AdminUser? IdUsuarioNavigation { get; set; }
    }
}
