using MediatR;

namespace byLiGG.Authorization.Models.Request
{
	public interface IBaseRequest<TResponse> : IRequest<TResponse>
	{

	}
}