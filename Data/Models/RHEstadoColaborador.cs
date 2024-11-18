namespace rrhh_backend.Data.Models
{
    public class RHEstadoColaborador
    {
        public int IdEstadoColaborador { get; set; }
        public string EstadosColaborador { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<RHColaborador> RHColaboradores { get; set; } = new List<RHColaborador>();
    }
}
