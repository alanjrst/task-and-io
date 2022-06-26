using System.ComponentModel.DataAnnotations;

namespace task.io.authentication.Application.DTOs.Users
{
    public class UserRequestChangePassword
    {
        [Required]
        [StringLength(255)]
        public string Email { get; set; }
        
        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}