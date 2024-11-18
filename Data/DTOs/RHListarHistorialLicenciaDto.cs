namespace rrhh_backend.Data.DTOs
{
    public class RHListarHistorialLicenciaDto
    {
        public int IdLicencia { get; set; }
        public string Nombres { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string ApellidoCasada { get; set; }
        public string NombreCompleto { get; set; }
        public string TipoLicencia { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Observaciones { get; set; }
        public string EstadoLicencia { get; set;}
        public string Departamento { get; set; }
    }
}
