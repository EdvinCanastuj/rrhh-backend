namespace rrhh_backend.Data.DTOs
{
    public class InsertColaboradorDto
    {
        public int IdDepartamento { get; set; }
        public int IdEstadoColaborador { get; set; }
        public int IdEstadoCivil { get; set; }
        public string? Codigo { get; set; }
        public string Dpi { get; set; }
        public string Nombres { get; set; }
        public string PrimerApellido { get; set; } = null!;
        public string? SegundoApellido { get; set; }
        public string? ApellidoCasada { get; set; }
        public string? MunicipioExtendido { get; set; }
        public string? DepartamentoExtendido { get; set; }
        public string? LugarNacimiento { get; set; }
        public string? Nacionalidad { get; set; }
        public string? NoIGSS { get; set; }
        public string? NoNIT { get; set; }
        public string? NombreConyuge { get; set; }
        public string? NoCuentaBancaria { get; set; }
        public string? Telefono { get; set; }
        public string ? Direccion { get; set; }
        public string Email { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime? FechaInicioLabores { get; set; }
    }
}
