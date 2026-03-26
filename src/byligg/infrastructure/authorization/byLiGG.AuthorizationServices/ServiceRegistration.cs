using byLiGG.AuthorizationServices.Abstractions.Modules.User.Services;
using byLiGG.AuthorizationServices.Modules.User;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace byLiGG.AuthorizationServices
{
	public static class ServiceRegistration
	{

		public static void AddAuthorizationServices(this IServiceCollection services)
		{
			var assembly = Assembly.GetExecutingAssembly();

			services.AddAutoMapper(cfg =>
			{
				cfg.ReplaceMemberName("_", "");

				cfg.AddMaps(assembly);
			});

			services.AddUserAuthorizationServices();
		}

		#region Behind the Scenes

		#region Settings

		private static void AddUserAuthorizationServices(this IServiceCollection services)
		{
			services.AddScoped<IUserAuthorizationService, UserAuthorizationService>();
		}

		#endregion

		#endregion

	}
}
