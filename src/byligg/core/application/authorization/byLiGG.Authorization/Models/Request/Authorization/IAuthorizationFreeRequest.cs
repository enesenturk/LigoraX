using byLiGG.Authorization.Models.Context.Authorization;

namespace byLiGG.Authorization.Models.Request.Authorization
{
	public interface IAuthorizationFreeRequest<TResponse> : IBaseRequest<TResponse>
	{

		AuthorizationFreeContext AuthorizationFree { get; set; }

	}
}