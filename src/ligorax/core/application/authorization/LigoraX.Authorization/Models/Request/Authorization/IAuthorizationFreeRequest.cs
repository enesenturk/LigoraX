using LigoraX.Authorization.Models.Context.Authorization;

namespace LigoraX.Authorization.Models.Request.Authorization
{
	public interface IAuthorizationFreeRequest<TResponse> : IBaseRequest<TResponse>
	{

		AuthorizationFreeContext AuthorizationFree { get; set; }

	}
}