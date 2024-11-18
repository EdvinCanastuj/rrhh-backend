namespace rrhh_backend.Data.Models
{
    public class RHEstadoLicencias
    {
        public int IdEstadoLicencia { get; set; }
        public string EstadoLicencia { get; set; }

        public string? Descripcion { get; set; }

        public virtual ICollection<RHLicencias> RHLicencias { get; set; } = new List<RHLicencias>();
    }
}
