using byLiGG.Application.Utilities.EntityProperty;
using Microsoft.Extensions.DependencyInjection;

namespace byLiGG.Application.Utilities
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
