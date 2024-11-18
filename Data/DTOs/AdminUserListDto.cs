namespace rrhh_backend.Data.DTOs
{
    public class AdminUserListDto
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Email { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdEstado { get; set; }
        public string? NombreEstado { get; set; }
        public int? IdColaborador { get; set; }
        public string Nombres { get; set; }
        public string PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }

    }
}
