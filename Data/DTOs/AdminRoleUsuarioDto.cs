namespace rrhh_backend.Data.DTOs
{
    public class AdminRoleUsuarioDto
    {
        public int IdRole { get; set; }
        public string NombreRol { get; set; } = null!;
        public int IdUsuario { get; set; }
        public string Email { get; set; } = null!;
        public string NombreUsuario { get; set; } = null!;
        public int IdColaborador { get; set; }
        public string Nombres { get; set; } = null!;
        public string PrimerApellido { get; set; } = null!;
        public string? SegundoApellido { get; set; }


    }
}
