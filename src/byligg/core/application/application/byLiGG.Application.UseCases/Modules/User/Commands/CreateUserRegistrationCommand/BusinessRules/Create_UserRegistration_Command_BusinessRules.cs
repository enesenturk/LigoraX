using Base.Exceptions.ExceptionModels;
using byLiGG.Domain.Entities;
using byLiGG.Domain.Language.Resources;
using byLiGG.Repositories.Abstractions.Modules;

namespace byLiGG.Application.UseCases.Modules.User.Commands.CreateUserRegistrationCommand.BusinessRules
{
	public class Create_UserRegistration_Command_BusinessRules
	{

		#region CTOR

		private readonly IUserRepository _userRepository;

		public Create_UserRegistration_Command_BusinessRules(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		#endregion

		public async Task EnsureUsernameAndEmailIsUnique(string username, string email)
		{
			List<t_user> duplicateds = await _userRepository.GetListAsync(
				orderBy: o => o.OrderBy(u => u.username),
				predicate: x => x.username == username || x.email == email
				);

			if (duplicateds.Count > 0)
			{
				if (duplicateds.Any(x => x.username == username))
					throw new BusinessRuleException(uiText.Username_Already_Taken);

				throw new BusinessRuleException(uiText.Email_Already_Registered);
			}
		}

	}
}