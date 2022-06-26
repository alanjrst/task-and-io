using System;
namespace task.io.authentication.Application.DTOs.Users
{
	public class UserResponseLogin
	{
        public string Email { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public bool Valid { get; set; }
	}
}

