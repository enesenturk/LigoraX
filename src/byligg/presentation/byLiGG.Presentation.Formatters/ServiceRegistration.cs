using byLiGG.Presentation.Formatters.DateTimeFormatters;
using byLiGG.Presentation.Formatters.NumberFormatters;
using Microsoft.Extensions.DependencyInjection;

namespace byLiGG.Presentation.Formatters
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
