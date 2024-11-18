using System.ComponentModel.DataAnnotations;

namespace rrhh_backend.Data.DTOs
{
    public class ResetPasswordRequestDto
    {
        [Required]
        [EmailAddress]
        // EmailRequestModel.cs (sin cambios)
        public string Email { get; set; }
    }
}
