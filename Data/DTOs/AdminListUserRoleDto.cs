namespace rrhh_backend.Data.DTOs
{
    public class AdminListUserRoleDto
    {
        public int IdUsuario { get; set; }
        public string Email { get; set; }
        public List<AdminRolListDto> RolList { get; set; }
    }
}
