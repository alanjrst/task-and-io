using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using task.io.authentication.Application.DTOs.Users;
using task.io.authentication.Application.Services.Common;
using task.io.authentication.Domain.Entities.Users;
using task.io.authentication.InfraStructure.Data;

namespace task.io.authentication.api.Services.Users
{
    public class UserService : IUserService
    {
        private readonly ILogger _logger;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService; 

        public UserService(ILogger<UserService> logger, IUserRepository userRepository, ITokenService tokenService)
        {
            _logger = logger;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<UserResponse> AddNewAsync(UserRequest model)
        {
            try
            {
                var userResult = await _userRepository.GetByEmailAsync(model.Email);
                if (userResult is not null)
                    throw new Exception("User already exists");

                var user = new User(model.UserName, model.FirstName, model.LastName, model.Email, model.Password, model.ImageAvatar);
                _userRepository.Create(user);
                await _userRepository.CommitAsync();

                var response = new UserResponse()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName =  user.LastName,
                    ImageAvatar= user.ImageAvatar
                };

                return response;

            }catch(Exception exception)
            {
                _logger.LogError("Error in add new user", exception);
                throw;
            }
        }

        public async Task<bool> ChangePasswordAsync(UserRequestChangePassword userRequestChangePassword)
        {
            if(string.IsNullOrEmpty(userRequestChangePassword.OldPassword) 
            && string.IsNullOrEmpty(userRequestChangePassword.NewPassword))
                return false;

            var user = await _userRepository.GetByEmailAsync(userRequestChangePassword.Email);
            if (!user.CheckPassword(userRequestChangePassword.OldPassword))
                return false;

            if(!user.ChangePassword(userRequestChangePassword.OldPassword, userRequestChangePassword.NewPassword))
            {
                return false;
            }

            _userRepository.Update(user);
            await _userRepository.CommitAsync();
            return true;

        }

        public async Task<UserResponseLogin> CheckLoginAsync(UserRequestLogin userRequestLogin){

            var user = await _userRepository.GetByEmailAsync(userRequestLogin.Email);
            if(user is null || !user.CheckPassword(userRequestLogin.Password))
                return new UserResponseLogin() { Valid = false };

            return new UserResponseLogin()
            {
                Email = userRequestLogin.Email,
                Name = user.FirstName,
                Token = _tokenService.TryGenerateTokenForAccount(userRequestLogin.Email, user.FirstName),
                Valid = true
            };
        }

        public async Task<UserResponse> EditAsync(UserRequest userRequest)
        {
            User user = await _userRepository.GetById(userRequest.Id);

            if (user is null)
            {
                _logger.LogWarning($"User not found. Id: {userRequest.Id}");
                throw new Exception($"User not found. Id: {userRequest.Id}");
            }


            user.Update(userRequest.UserName, 
                        userRequest.FirstName, 
                        userRequest.LastName,
                        userRequest.ImageAvatar);

            _userRepository.Update(user);
            await _userRepository.CommitAsync();
            
            return new UserResponse(){
                                    Id = user.Id,
                                    FirstName = user.FirstName,
                                    LastName = user.LastName,
                                    Email = user.Email,
                                    ImageAvatar = user.ImageAvatar,
                                    UserName = user.UserName
                                };
        }

        public async Task<bool> RequestResetPassword(UserRequest userRequest)
        {
            User user = await _userRepository.GetByEmailAsync(userRequest.Email);
            if (user == null)
                return false;

            user.SetTokenRememberPassword();

            _userRepository.Update(user);
            await _userRepository.CommitAsync();

            //send email
            return true;
        }

        public async Task<bool> RestorePassword(string email, string token, string password)
        {
            User user = await _userRepository.GetByEmailAndToken(email, token);
            if (user == null)
                return false;

            bool result = user.RestorePassword(token, password);
            if (!result)
                return false;

            _userRepository.Update(user);
            await _userRepository.CommitAsync();

            return true;
        }
    }
}