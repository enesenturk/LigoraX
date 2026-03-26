using FluentValidation;
using byLiGG.Application.UseCases.Modules.User.Commands.CreateUserRegistrationCommand.Dtos;
using byLiGG.Domain.Language.Helpers;

namespace byLiGG.Application.UseCases.Modules.User.Commands.CreateUserRegistrationCommand.Validators
{
	public class Create_UserRegistration_CommandValidator : AbstractValidator<Create_UserRegistration_CommandDto>
	{

		public Create_UserRegistration_CommandValidator()
		{
			RuleFor(x => x.Username)
				.NotEmpty()
				.MinimumLength(3)
				.MaximumLength(30)
				.Matches(@"^[a-zA-Z0-9_.]+$");

			RuleFor(x => x.Email)
				.NotEmpty()
				.EmailAddress()
				.MaximumLength(254);

			RuleFor(x => x.Password)
				.NotEmpty()
				.MinimumLength(6)
				.MaximumLength(100)
				.Matches(@"[a-zA-Z]")
				.Matches(@"[0-9]");

			RuleFor(x => x.DisplayName)
				.NotEmpty()
				.MinimumLength(2)
				.MaximumLength(60);

			RuleFor(x => x.LanguagePreference)
				.NotEmpty()
				.Must(LanguageHelper.IsSupportedLanguage);
		}
	}
}
