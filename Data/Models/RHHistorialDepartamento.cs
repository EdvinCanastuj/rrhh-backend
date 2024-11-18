namespace rrhh_backend.Data.Models
{
    public class RHHistorialDepartamento
    {
        public int IdHistorialDepartamento { get; set; }
        public int IdColaborador { get; set; }
        public int IdDepartamento { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public virtual RHColaborador RHColaborador { get; set; } = null!;
        public virtual RHDepartamento RHDepartamento { get; set; } = null!;
    }
}