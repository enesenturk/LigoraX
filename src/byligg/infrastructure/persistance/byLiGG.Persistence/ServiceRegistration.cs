using byLiGG.Configuration.AppSettings;
using byLiGG.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace byLiGG.Persistence
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
		{
			byliggContext.connectionString = DataBaseSettings.ConnectionString;

			return services;
		}
	}
}