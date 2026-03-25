using LigoraX.Authorization.Models.Context.Authorization;

namespace LigoraX.Authorization.Models.Request.Authorization
{
	public interface IAuthorizationRequest<TResponse> : IBaseRequest<TResponse>
	{

		AuthorizationContext Authorization { get; set; }

	}
}