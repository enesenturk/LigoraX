using LigoraX.Authorization.Models.Context.Authentication;

namespace LigoraX.Authorization.Models.Request.Authentication
{
	public interface IAuthenticationFreeRequest<TResponse> : IBaseRequest<TResponse>
	{
		AuthenticationFreeContext AuthenticationFree { get; set; }
	}
}