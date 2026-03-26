using Base.Exceptions.ExceptionModels;
using byLiGG.Authorization.Models.Request.Authorization;
using MediatR;

namespace byLiGG.Authorization.Pipelines
{
	public class AuthorizationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
	{

		#region CTOR

		#endregion

		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			if (request is not IAuthorizationRequest<TResponse> && request is not IAuthorizationFreeRequest<TResponse>)
				throw new AbsurdOperationException("All UseCases must be of type IAuthorizationRequest or IAuthorizationFreeRequest and routed through IMediator.");

			if (request is IAuthorizationRequest<TResponse> authorizationRequest)
			{
				if (authorizationRequest.Authorization.ExecutingUserId == Guid.Empty)
					throw new AuthorizationException();

				//
			}
			else if (request is IAuthorizationFreeRequest<TResponse> authorizationFreeRequest)
			{
				if (authorizationFreeRequest.AuthorizationFree is null || authorizationFreeRequest.AuthorizationFree.ExecutingUserId == Guid.Empty)
					throw new AuthorizationException();

				//
			}

			TResponse response = await next();

			return response;
		}
	}

}
