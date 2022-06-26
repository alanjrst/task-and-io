using System;

namespace task.io.authentication.Domain.Entities.Users
{
    public partial class User
    {
        public User(){
        }

        public User(string userName, string firstName, string lastName, string email, string password, string imageAvatar)
        {
            Password = HashGeneration(password);
            Email = email;
            CreatedDate = DateTime.Now;
            DeletedDate = DateTime.Parse("0001-01-01");
            this.Update(userName, firstName, lastName, imageAvatar);
        }

        public void Update(string userName, string firstName, string lastName, string imageAvatar){
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            ImageAvatar = imageAvatar;
            UpdatedDate = DateTime.Now;
        }

        public bool ChangePassword(string oldPassword,string newPassword){

            if(CheckPassword(oldPassword))
            {
                Password = HashGeneration(newPassword);
                return true;
            }

            return false;
        }

        public bool CheckPassword(string password){
            return BCrypt.Net.BCrypt.Verify(password, Password);
        }

        public bool SetTokenRememberPassword()
        {
            TokenRememberPassword = BCrypt.Net.BCrypt.HashPassword(DateTime.Now.ToString());
            return true;
        }

        public bool RestorePassword(string tokenRememberPassword, string password){

            if(TokenRememberPassword == tokenRememberPassword){
                Password = HashGeneration(password);
                return true;
            }

            return false;
        }

        public static string HashGeneration(string password)
        {
            int workfactor = 10;

            string salt = BCrypt.Net.BCrypt.GenerateSalt(workfactor);
            string hash = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return hash;
        }
    }
}