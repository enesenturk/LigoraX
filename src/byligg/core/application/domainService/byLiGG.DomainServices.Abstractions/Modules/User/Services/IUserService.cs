using byLiGG.Domain.Entities;

namespace byLiGG.DomainServices.Abstractions.Modules.User.Services
{
	public interface IUserService
	{

		#region Create

		Task<Guid> RegisterAsync(t_user user);
		Task SuccessfulLoginAsync(t_user user, string ipAddress);

		#endregion

	}
}