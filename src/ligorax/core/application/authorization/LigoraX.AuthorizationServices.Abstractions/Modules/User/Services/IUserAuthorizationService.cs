namespace LigoraX.AuthorizationServices.Abstractions.Modules.User.Services
{
	public interface IUserAuthorizationService
	{

		#region Create

		string GenerateToken(Guid userId, string username, string email);

		#endregion

	}
}