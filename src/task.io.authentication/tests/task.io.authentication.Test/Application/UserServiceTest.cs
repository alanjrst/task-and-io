using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using task.io.authentication.api.Services.Users;
using task.io.authentication.Application.DTOs.Users;
using task.io.authentication.Application.Services.Common;
using task.io.authentication.Domain.Entities.Users;

namespace task.io.authentication.Test.Application
{
	public class UserServiceTest
	{
		private IUserService _userService;
		private IUserRepository _userRepository;
		private ITokenService _tokenService;
		private UserRequest _userRequest;
		private UserRequestChangePassword _userRequestChangePassword;
		public User _user { get; set; }

		[SetUp]
		public void SetUp()
		{
			ILogger<UserService> logger = Substitute.For<ILogger<UserService>>();
			_userRepository = Substitute.For<IUserRepository>();
			_tokenService = Substitute.For<ITokenService>();

			_userService = new UserService(logger, _userRepository, _tokenService);

			_userRequest = new UserRequest()
			{
				Email = "teste@teste.com",
				FirstName = "FirstName",
				LastName = "LastName",
				ImageAvatar = "Image.png",
				Password = "password",
				UserName = "UserName"
			};

			_userRequestChangePassword = new UserRequestChangePassword()
			{
				Email = "teste@teste.com",
				NewPassword = "NewPassword",
				OldPassword = "password"
			};

			_user = new User("Name", "firstName", "lastName", "email@email.com", "password", String.Empty);
		
		
			_userRepository.ClearReceivedCalls();
		}

		[Test]
		public async Task ShouldAddNewUser()
		{
			// Action
			UserResponse userResponse = await _userService.AddNewAsync(_userRequest);

			// Assert
			Assert.AreEqual(userResponse.FirstName, _userRequest.FirstName);
			Assert.AreEqual(userResponse.LastName, _userRequest.LastName);
			Assert.AreEqual(userResponse.ImageAvatar, _userRequest.ImageAvatar);
			Assert.AreEqual(userResponse.Email, _userRequest.Email);
			Assert.AreEqual(userResponse.UserName, _userRequest.UserName);
		}

		[Test]
		public void NotShouldAddNewUser()
		{
			// Arrange
			_userRepository.GetByEmailAsync(Arg.Any<string>()).Returns(Task.FromResult(new User()));

			// Assert
			Assert.ThrowsAsync<Exception>(() =>  _userService.AddNewAsync(_userRequest));
		}

		[Test]
		public async Task ShouldChangePasswordAsync()
        {
			// Arrange
			_userRepository.GetByEmailAsync(Arg.Any<string>()).Returns(Task.FromResult(_user));

			// Action
			var result = await _userService.ChangePasswordAsync(_userRequestChangePassword);

			// Assert
			Assert.AreEqual(result, true);
        }

        //[Test]
        //public async Task NotShouldChangePasswordAsync()
        //{
        //    // Arrange
        //    _userRequestChangePassword.OldPassword = "invalidPassword";
        //    _userRepository.GetByEmailAsync(Arg.Any<string>()).Returns(Task.FromResult(_user));

        //    // Action
        //    var result = await _userService.ChangePasswordAsync(_userRequestChangePassword);

        //    // Assert
        //    Assert.AreEqual(result, true);
        //}
    }
}

