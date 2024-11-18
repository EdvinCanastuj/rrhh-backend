namespace rrhh_backend.Data.DTOs
{
    public class AdminNewUserDto
    {
        public string nombreUsuario { get; set; } = null!;
        public string password { get; set; } = null!;
        public string email { get; set; }
        public int? idColaborador { get; set; }
    }
}
