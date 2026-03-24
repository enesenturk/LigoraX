using LigoraX.Presentation.Modules.User;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LigoraX.Presentation
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddPresentationServices(this IServiceCollection services)
		{
			var assembly = Assembly.GetExecutingAssembly();

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
			services.AddScoped<UserControllerInternal>();
		}

		#endregion

	}
}
