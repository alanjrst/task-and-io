using System.ComponentModel.DataAnnotations;

namespace task.io.authentication.Application.DTOs.Users
{
    public class UserResponse
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(125)]
        public string LastName { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        public string ImageAvatar { get; set; }

    }
}