using LigoraX.Application.Utilities.EntityProperty;
using Microsoft.Extensions.DependencyInjection;

namespace LigoraX.Application.Utilities
{
	public static class ServiceRegistration
	{

		public static IServiceCollection AddApplicationUtilityServices(this IServiceCollection services)
		{
			services.AddScoped<EntityPropertySetter>();

			return services;
		}

	}
}
