using AutoMapper;
using Base.Exceptions.ExceptionModels;
using Base.PrimitiveTypeHelpers._DateTime.Entensions;
using Base.Security.Services;
using byLiGG.Application.UseCases.Modules.User.Queries.GetUserLoginQuery.Dtos;
using byLiGG.Application.Utilities.Mediator;
using byLiGG.AuthorizationServices.Abstractions.Modules.User.Services;
using byLiGG.Domain.Entities;
using byLiGG.Domain.Language.Resources;
using byLiGG.DomainServices.Abstractions.Modules.User.Services;
using byLiGG.Repositories.Abstractions.Modules;

namespace byLiGG.Application.UseCases.Modules.User.Queries.GetUserLoginQuery
{
	public class Get_UserLogin_QueryHandler : UseCaseHandler<Get_UserLogin_QueryDto, Get_UserLogin_ResponseDto>
	{

		#region CTOR

		private readonly IUserRepository _userRepository;

		private readonly IUserService _userService;

		private readonly IUserAuthorizationService _userAuthorizationService;

		public Get_UserLogin_QueryHandler(
			IMapper mapper,

			IUserRepository userRepository,
			IUserService userService,
			IUserAuthorizationService userAuthorizationService) : base(mapper)
		{
			_userRepository = userRepository;
			_userService = userService;
			_userAuthorizationService = userAuthorizationService;
		}

		#endregion

		public override async Task<Get_UserLogin_ResponseDto> Handle(Get_UserLogin_QueryDto query, CancellationToken cancellationToken)
		{
			t_user user = await _userRepository.GetAsync(
				predicate: x => x.username == query.Username
				);

			if (user is null || !HashService.VerifyHash(query.Password, user.password_hash))
				throw new BusinessRuleException(uiText.Invalid_Credentials);

			if (!user.is_active)
				throw new BusinessRuleException(uiText.Account_Inactive);

			user.last_login_at = DateTime.Now.ToUniversalTimeZone();

			await _userService.SuccessfulLoginAsync(user, query.IpAddress);

			return new Get_UserLogin_ResponseDto
			{
				UserId = user.id,
				Username = user.username,
				Email = user.email,
				DisplayName = user.display_name,
				Token = _userAuthorizationService.GenerateToken(user.id, user.username, user.email)
			};
		}

	}
}