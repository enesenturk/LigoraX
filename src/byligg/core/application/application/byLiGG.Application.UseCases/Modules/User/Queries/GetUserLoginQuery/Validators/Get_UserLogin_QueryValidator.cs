using FluentValidation;
using byLiGG.Application.UseCases.Modules.User.Queries.GetUserLoginQuery.Dtos;

namespace byLiGG.Application.UseCases.Modules.User.Queries.GetUserLoginQuery.Validators
{
	public class Get_UserLogin_QueryValidator : AbstractValidator<Get_UserLogin_QueryDto>
	{

		public Get_UserLogin_QueryValidator()
		{
			RuleFor(x => x.Username)
				.NotEmpty();

			RuleFor(x => x.Password)
				.NotEmpty();
		}
	}
}