namespace rrhh_backend.Data.DTOs
{
    public class UserRolesPermisosDto
    {
        public int IdRole { get; set; }
        public string NombreRol { get; set; }
        public string DescripcionRol { get; set; }
        public List<PermisoDto> Permisos { get; set; }
    }
}
