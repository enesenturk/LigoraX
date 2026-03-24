using Base.Session.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Session
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddSessionServices(this IServiceCollection services)
		{
			services.AddSession();

			services.AddSingleton<ISessionService, SessionService>();

			return services;
		}
	}
}
