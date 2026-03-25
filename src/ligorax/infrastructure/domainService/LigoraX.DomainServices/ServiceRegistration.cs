using LigoraX.DomainServices.Abstractions.Modules.System.Property.Services;
using LigoraX.DomainServices.Abstractions.Modules.User.Services;
using LigoraX.DomainServices.Modules.System.Property;
using LigoraX.DomainServices.Modules.User;
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

			services.AddServices();
		}

		#region Behind the Scenes

		private static void AddServices(this IServiceCollection services)
		{
			services.AddScoped<ISystemPropertyService, SystemPropertyService>();

			services.AddScoped<IUserService, UserService>();
		}

		#endregion

	}
}