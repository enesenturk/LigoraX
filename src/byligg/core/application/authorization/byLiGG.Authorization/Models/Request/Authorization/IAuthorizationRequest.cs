using byLiGG.Authorization.Models.Context.Authorization;

namespace byLiGG.Authorization.Models.Request.Authorization
{
	public interface IAuthorizationRequest<TResponse> : IBaseRequest<TResponse>
	{

		AuthorizationContext Authorization { get; set; }

	}
}