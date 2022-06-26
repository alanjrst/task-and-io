using System.ComponentModel.DataAnnotations;

namespace task.io.authentication.Application.DTOs.Users
{
    public class UserRequestLogin
    {
        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }
    }
}