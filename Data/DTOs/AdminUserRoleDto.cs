namespace rrhh_backend.Data.DTOs
{
    public class AdminUserRoleDto
    {
        public string nombreRol { get; set; } = null!;
        public string descripcionRol { get; set; } = null!;
        public int? idmodulo { get; set; }
    }
}
