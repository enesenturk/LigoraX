using Microsoft.Extensions.DependencyInjection;

namespace LigoraX.Persistence
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
		{
			//CMS_DEVContext.connectionString = DataBaseSettings.ConnectionString;

			return services;
		}
	}
}