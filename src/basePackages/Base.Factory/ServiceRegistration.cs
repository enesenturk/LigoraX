using Base.Factory.Instance;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Factory
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddFactoryServices(this IServiceCollection services)
		{
			services.AddScoped<IInstanceFactory, InstanceFactory>();

			return services;
		}
	}
}