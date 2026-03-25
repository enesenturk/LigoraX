using LigoraX.Configuration.AppSettings;
using LigoraX.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace LigoraX.Persistence
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
		{
			ligoraxContext.connectionString = DataBaseSettings.ConnectionString;

			return services;
		}
	}
}