using System;
using task.io.authentication.Domain.Base;

namespace task.io.authentication.Domain.Entities.Users
{
    public partial class User : BaseEntity
    {
        public string UserName { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get;  private set; }
        public string Password { get; private set; }
        public string ImageAvatar { get; private set; }
        public string TokenRememberPassword { get; private set; }
    }
}