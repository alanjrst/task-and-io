using System;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace task.io.authentication.Application.Services.Common
{
	public class TokenService : ITokenService
    {
        private readonly ILogger _logger;

        public TokenService(ILogger<TokenService> logger)
		{
            _logger = logger;
        }

        public string TryGenerateTokenForAccount(string email, string name)
        {
            try
            {
                var secret = Environment.GetEnvironmentVariable("JWT_TOKEN_SECRET");
                var expiration = Environment.GetEnvironmentVariable("JWT_TOKEN_EXPIRATION_TIME");

                if (!int.TryParse(expiration, out int expirationTimeInDays))
                    expirationTimeInDays = 1;

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Name, name)
                    }),
                    Expires = DateTime.Now.AddDays(expirationTimeInDays),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    NotBefore = DateTime.Now
                };
                return tokenHandler.CreateEncodedJwt(tokenDescriptor);
            }
            catch (Exception exception)
            {
                _logger.LogError("Error in add new user", exception);
                throw new Exception("Not was possible generation token");
            }
        }
    }
}
