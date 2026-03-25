using MediatR;

namespace LigoraX.Authorization.Models.Request
{
	public interface IBaseRequest<TResponse> : IRequest<TResponse>
	{

	}
}