using byLiGG.Authorization.Models.Context.Authentication;

namespace byLiGG.Authorization.Models.Request.Authentication
{
	public interface IAuthenticationFreeRequest<TResponse> : IBaseRequest<TResponse>
	{
		AuthenticationFreeContext AuthenticationFree { get; set; }
	}
}