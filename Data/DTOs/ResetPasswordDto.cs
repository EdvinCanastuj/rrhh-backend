namespace rrhh_backend.Data.DTOs
{
    public class ResetPasswordDto
    {
        public string ResetToken { get; set; }
        public string NewPassword { get; set; }
    }
}
