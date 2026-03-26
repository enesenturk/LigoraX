using Base.Validation;
using byLiGG.Authorization.Models.Request.Authentication;

namespace byLiGG.Application.UseCases.Modules.User.Queries.GetUserLoginQuery.Dtos
{
	public class Get_UserLogin_QueryDto : AuthenticationFreeRequest, IAuthenticationFreeRequest<Get_UserLogin_ResponseDto>, IValidationRequest
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string IpAddress { get; set; }
	}
}
