using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace byLiGG.Presentation
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddPresentationServices(this IServiceCollection services)
		{
			Assembly assembly = Assembly.GetExecutingAssembly();

			services.AddAutoMapper(cfg =>
			{
				cfg.ReplaceMemberName("_", "");
				cfg.AddMaps(assembly);
			});

			services.AddControllerInternals();

			return services;
		}

		#region Behind the Scenes

		private static void AddControllerInternals(this IServiceCollection services)
		{

		}

		#endregion

	}
}
