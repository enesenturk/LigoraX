using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LigoraX.DomainServices
{
	public static class ServiceRegistration
	{

		public static void AddDomainServices(this IServiceCollection services)
		{
			var assembly = Assembly.GetExecutingAssembly();

			services.AddAutoMapper(cfg =>
			{
				cfg.ReplaceMemberName("_", "");

				cfg.AddMaps(assembly);
			});

		}

	}
}