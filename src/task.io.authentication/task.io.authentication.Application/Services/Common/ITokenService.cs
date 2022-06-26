using System;
namespace task.io.authentication.Application.Services.Common
{
	public interface ITokenService
	{
		string TryGenerateTokenForAccount(string email, string name);
	}
}

