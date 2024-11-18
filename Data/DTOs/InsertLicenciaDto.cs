namespace rrhh_backend.Data.DTOs
{
    public class InsertLicenciaDto
    {
        public int IdColaborador { get; set; }
        public int IdTipoLicencia { get; set; }
        public int IdEstadoLicencia { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string? Observaciones { get; set; }
    }
}
