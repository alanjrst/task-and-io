using NUnit.Framework;
using task.io.authentication.Domain.Entities.Users;

namespace task.io.authentication.Test.Domain;

public class UserTest
{
    public User _user { get; set; }

    public UserTest()
    {
		_user = new User("Name", "firstName", "lastName", "email@email.com", "Password@645645", String.Empty);
	}

	[Test]
	public void ShouldCreateUser()
	{
		// Assert
		Assert.AreEqual("Name", _user.UserName);
		Assert.AreEqual("firstName", _user.FirstName);
		Assert.AreEqual("lastName", _user.LastName);
		Assert.AreEqual("email@email.com", _user.Email);
		Assert.AreEqual(string.Empty, _user.ImageAvatar);
	}

	[Test]
	public void ShouldUpdateUser()
	{
		// Act
		_user.Update("userNameUpdate", "firstNameUpdate", "lastNameUpdate", "avatarUpdate");

		// Assert
		Assert.AreEqual("userNameUpdate", _user.UserName);
		Assert.AreEqual("firstNameUpdate", _user.FirstName);
		Assert.AreEqual("lastNameUpdate", _user.LastName);
		Assert.AreEqual("email@email.com", _user.Email);
		Assert.AreEqual("avatarUpdate", _user.ImageAvatar);
	}

	[TestCase("Password@645645", ExpectedResult=true)]
	[TestCase("PasswordInvalid", ExpectedResult=false)]
	public bool ShouldChangePassword(string password)
	{
        return _user.ChangePassword(password, "NewPassword");
	}

	[Test]
	public void ShouldRequestResetPassword()
    {
		string oldToken = _user.TokenRememberPassword;

		_user.SetTokenRememberPassword();

		// Assert
		Assert.AreNotEqual(oldToken, _user.TokenRememberPassword);
    }

	[Test]
	public void ShouldRestorePassword()
    {
		// Arrange
		string password = "newPassword";
		_user.SetTokenRememberPassword();

		// Act
		bool resultRestorePassword = _user.RestorePassword(_user.TokenRememberPassword, password);
		bool resultCheckPassword = _user.CheckPassword(password);

		// Assert
		Assert.True(resultRestorePassword);
		Assert.True(resultCheckPassword);
    }
}

