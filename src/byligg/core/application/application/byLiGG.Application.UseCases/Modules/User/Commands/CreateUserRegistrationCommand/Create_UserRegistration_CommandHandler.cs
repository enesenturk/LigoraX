using AutoMapper;
using Base.Security.Services;
using byLiGG.Application.UseCases.Modules.User.Commands.CreateUserRegistrationCommand.BusinessRules;
using byLiGG.Application.UseCases.Modules.User.Commands.CreateUserRegistrationCommand.Dtos;
using byLiGG.Application.Utilities.Mediator;
using byLiGG.Domain.Entities;
using byLiGG.DomainServices.Abstractions.Modules.User.Services;

namespace byLiGG.Application.UseCases.Modules.User.Commands.CreateUserRegistrationCommand
{
	public class Create_UserRegistration_CommandHandler : UseCaseHandler<Create_UserRegistration_CommandDto, Create_UserRegistration_ResponseDto>
	{

		#region CTOR

		private readonly IUserService _userService;

		private readonly Create_UserRegistration_Command_BusinessRules _businessRules;

		public Create_UserRegistration_CommandHandler(
			IMapper mapper,
			IUserService userService,
			Create_UserRegistration_Command_BusinessRules businessRules) : base(mapper)
		{
			_userService = userService;
			_businessRules = businessRules;
		}

		#endregion

		public override async Task<Create_UserRegistration_ResponseDto> Handle(
			Create_UserRegistration_CommandDto command, CancellationToken cancellationToken)
		{
			await _businessRules.EnsureUsernameAndEmailIsUnique(command.Username, command.Email);

			t_user user = _mapper.Map<t_user>(command);
			user.password_hash = HashService.Hash(command.Password);

			Guid userId = await _userService.RegisterAsync(user);

			Create_UserRegistration_ResponseDto response = _mapper.Map<Create_UserRegistration_ResponseDto>(user);
			response.UserId = userId;

			return response;
		}

	}
}