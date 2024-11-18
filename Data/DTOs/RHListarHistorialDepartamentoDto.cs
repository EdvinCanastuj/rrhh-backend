namespace rrhh_backend.Data.DTOs
{
    public class RHListarHistorialDepartamentoDto
    {
        public int IdHistorialDepartamento { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Departamento { get; set; }
        public string Nombres { get; set; }
        public string PrimerApellido { get; set; }
        public string? SegundoApellido { get; set;}
        public string? ApellidoCasada { get; set; }
        public string NombreCompleto { get; set; }

    }
}
