using Base.DataAccess.Aspects.TransactionalOperation;
using LigoraX.Application.Constants;
using LigoraX.Domain.Entities;
using LigoraX.DomainServices.Abstractions.Modules.User.Services;
using LigoraX.Repositories.Abstractions.Modules;

namespace LigoraX.DomainServices.Modules.User
{
	public class UserService : IUserService
	{

		#region CTOR

		private readonly IUserRepository _userRepository;
		private readonly IUserStatRepository _userStatRepository;
		private readonly ILoginLogRepository _loginLogRepository;

		public UserService(
			IUserRepository userRepository,
			IUserStatRepository userStatRepository,
			ILoginLogRepository loginLogRepository)
		{
			_userRepository = userRepository;
			_userStatRepository = userStatRepository;
			_loginLogRepository = loginLogRepository;
		}

		#endregion

		#region Create

		[TransactionalOperationAspect]
		public async Task<Guid> RegisterAsync(t_user user)
		{
			t_user createdUser = await _userRepository.AddAsync(user, SystemConstants.SystemUserId);

			t_user_stat userStat = new t_user_stat
			{
				t_user_id = createdUser.id
			};

			await _userStatRepository.AddAsync(userStat, SystemConstants.SystemUserId);

			return createdUser.id;
		}

		[TransactionalOperationAspect]
		public async Task SuccessfulLoginAsync(t_user user, string ipAddress)
		{
			await _userRepository.UpdateAsync(
				user, user.id, nameof(t_user.last_login_at)
				);

			await _loginLogRepository.AddAsync(new t_login_log
			{
				t_user_id = user.id,
				ip_address = ipAddress,
				is_success = true,
			}, user.id);
		}

		#endregion

	}
}
