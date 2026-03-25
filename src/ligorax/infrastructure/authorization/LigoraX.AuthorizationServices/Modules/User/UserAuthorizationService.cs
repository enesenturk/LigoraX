using LigoraX.AuthorizationServices.Abstractions.Modules.User.Services;
using LigoraX.Configuration.AppSettings;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LigoraX.AuthorizationServices.Modules.User
{
	public class UserAuthorizationService : IUserAuthorizationService
	{

		#region Create

		public string GenerateToken(Guid userId, string username, string email)
		{
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.SecretKey));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
				new Claim(JwtRegisteredClaimNames.UniqueName, username),
				new Claim(JwtRegisteredClaimNames.Email, email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			};

			var token = new JwtSecurityToken(
				issuer: JwtSettings.Issuer,
				audience: JwtSettings.Audience,
				claims: claims,
				expires: DateTime.UtcNow.AddDays(JwtSettings.ExpiryDays),
				signingCredentials: credentials
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		#endregion

	}
}
