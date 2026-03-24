using LigoraX.Presentation.Formatters.DateTimeFormatters;
using LigoraX.Presentation.Formatters.NumberFormatters;
using Microsoft.Extensions.DependencyInjection;

namespace LigoraX.Presentation.Formatters
{
	public static class ServiceRegistration
	{

		public static IServiceCollection AddPresentationFormatterServices(this IServiceCollection services)
		{
			services.AddFormatters();

			return services;
		}

		#region Behind the Scenes

		private static void AddFormatters(this IServiceCollection services)
		{
			services.AddScoped<IDateTimeFormatter, DateTimeFormatter>();
			services.AddScoped<INumberFormatter, NumberFormatter>();
		}

		#endregion

	}
}
