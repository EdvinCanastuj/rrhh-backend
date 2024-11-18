namespace rrhh_backend.Data.DTOs
{
    public class AdminRolListDto
    {
        public int IdRole { get; set; }
        public string NombreRol { get; set; }
        public string? DescripcionRol { get; set; }
        public string? NombreModulo { get; set; }

        public int? IdModulos { get; set; }
    }
}
