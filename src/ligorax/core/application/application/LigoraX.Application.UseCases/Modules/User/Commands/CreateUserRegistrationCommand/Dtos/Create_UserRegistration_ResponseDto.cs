namespace LigoraX.Application.UseCases.Modules.User.Commands.CreateUserRegistrationCommand.Dtos
{
	public class Create_UserRegistration_ResponseDto
	{
		public Guid UserId { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public string DisplayName { get; set; }
	}
}
