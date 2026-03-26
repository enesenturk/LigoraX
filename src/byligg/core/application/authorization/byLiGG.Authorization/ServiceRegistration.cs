using byLiGG.Authorization.Pipelines;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace byLiGG.Authorization
{
	public static class ServiceRegistration
	{

		public static void AddAuthorizationBehavior(this IServiceCollection services)
		{
			services.AddScoped(typeof(IPipelineBehavior<,>), typeof(AuthorizationPipelineBehavior<,>));
		}

	}
}
