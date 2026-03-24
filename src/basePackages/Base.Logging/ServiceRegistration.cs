using Base.Logging.Loggers;
using Base.Logging.Loggers.Microsoft;
using Base.Logging.Pipelines;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Logging
{
	public static class ServiceRegistration
	{

		public static IServiceCollection AddLoggingServices(this IServiceCollection services)
		{
			services.AddSingleton<ILoggerService, MicrosoftConsoleLogger>();

			services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

			return services;
		}

		public static IApplicationBuilder AddLoggingServices(this IApplicationBuilder app)
		{
			//SerilogConsoleLogger._logger = app.ApplicationServices.GetService<ILogger>();

			return app;
		}

	}
}
