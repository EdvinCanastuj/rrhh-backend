namespace rrhh_backend.Data.Models
{
    public class RHDepartamento
    {
        public int IdDepartamentos { get; set; }

        public string NombreDepartamento { get; set; } = null!;

        public string? DescripcionDepartamento { get; set; }

        public virtual ICollection<RHColaborador> RHColaboradores { get; set; } = new List<RHColaborador>();

        public virtual ICollection<RHHistorialDepartamento> RHHistorialDepartamentos { get; set; } = new List<RHHistorialDepartamento>();


    }
}
