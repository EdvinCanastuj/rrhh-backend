namespace rrhh_backend.Data.Models
{
    public class RHTipoLicencias
    {
        public int IdTipoLicencia { get; set; }
        public string TipoLicencia { get; set; }

        public string? Descripcion { get; set; }

        public virtual ICollection<RHLicencias> RHLicencias { get; set; } = new List<RHLicencias>();
    }
}
