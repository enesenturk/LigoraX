using Base.Validation;
using LigoraX.Authorization.Models.Request.Authentication;

namespace LigoraX.Application.UseCases.Modules.User.Commands.CreateUserRegistrationCommand.Dtos
{
	public class Create_UserRegistration_CommandDto : AuthenticationFreeRequest, IAuthenticationFreeRequest<Create_UserRegistration_ResponseDto>, IValidationRequest
	{
		public string Username { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string DisplayName { get; set; }
		public string LanguagePreference { get; set; }
	}
}