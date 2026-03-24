using Base.Cookie.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Cookie
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddCookieServices(this IServiceCollection services, TimeSpan defaultExpiration)
		{
			services.AddSingleton<ICookieService>(provider =>
			{
				var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();

				return new CookieService(httpContextAccessor, defaultExpiration);
			});

			return services;
		}
	}
}
