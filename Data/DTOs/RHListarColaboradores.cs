namespace rrhh_backend.Data.DTOs
{
    public class RHListarColaboradores
    {
        public int IdColaborador { get; set; }
        public int IdDepartamento { get; set; }
        public string NombreDepartamento { get; set; } = null!;
        public int IdEstadoColaborador { get; set; }
        public string EstadoColaborador { get; set; } = null!;
        public int IdEstadoCivil { get; set; }
        public string? Codigo { get; set; } = null!;
        public string Dpi { get; set; }
        public string Nombres { get; set; } = null!;
        public string PrimerApellido { get; set; } = null!;
        public string? SegundoApellido { get; set; } = null!;
        public string? ApellidoCasada { get; set; } = null!;
        public string? MunicipioExtendido { get; set; } = null!;
        public string? DepartamentoExtendido { get; set; } = null!;
        public string? LugarNacimiento { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? NoCuentaBancaria { get; set; }
        public string? Nacionalidad { get; set; }
        public string? NoIGSS { get; set; }
        public string? NoNIT { get; set; }
        public string? NombreConyuge { get; set; }
        public string? Direccion { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public string Email { get; set; } = null!;
        public DateTime? FechaInicioLabores { get; set; }
        public DateTime? Debaja { get; set; }
        public string? Foto { get; set; }
    }
}
