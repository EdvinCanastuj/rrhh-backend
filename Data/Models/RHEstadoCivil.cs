namespace rrhh_backend.Data.Models
{
    public class RHEstadoCivil
    {
        public int IdEstadoCivil { get; set; }
        public string EstadoCivil { get; set; } = null!;
        public virtual ICollection<RHColaborador> RHColaboradores { get; set; } = new List<RHColaborador>();

    }
}
