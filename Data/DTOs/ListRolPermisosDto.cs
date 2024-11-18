namespace rrhh_backend.Data.DTOs
{
    public class ListRolPermisosDto
    {
        public int IdRole { get; set; }
        public string NombreRol { get; set; }
        public List<PermisoDto> Permisos { get; set; }
    }
}
