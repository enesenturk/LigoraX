using Base.Caching;
using Base.Cookie;
using Base.Factory;
using Base.Logging;
using Base.Logging.Loggers;
using LigoraX.AdapterServices;
using LigoraX.Application.Utilities;
using LigoraX.Application.Utilities.EntityProperty;
using LigoraX.AuthorizationServices;
using LigoraX.Configuration.Registration;
using LigoraX.DomainServices;
using LigoraX.Persistence;
using LigoraX.Presentation;
using LigoraX.Presentation.Constants.CookieConstants;
using LigoraX.Presentation.Formatters;
using LigoraX.Repositories;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;

namespace LigoraX.API
{
	public static class AppBuilder
	{

		public static WebApplicationBuilder BuildApplication(this WebApplicationBuilder builder)
		{
			builder.Services.AddHttpContextAccessor();
			builder.Services.AddControllers();

			builder.AddConfigurations();
			builder.Services.AddAdapterServices();
			builder.Services.AddAuthorizationServices();
			builder.Services.AddApplicationUtilityServices();
			builder.Services.AddCacheServices();
			builder.Services.AddCookieServices(TimeSpan.FromDays(DefaultCookieConstants.Default_TimeOutDays));
			builder.Services.AddFactoryServices();
			builder.Services.AddDomainServices();
			builder.Services.AddRepositoryServices();
			builder.Services.AddPersistenceServices();
			builder.Services.AddPresentationFormatterServices();
			builder.Services.AddPresentationServices();

			builder.Services.AddLoggingServices();

			builder.Services.Configure<FormOptions>(options =>
			{
				options.ValueCountLimit = 5000;
			});

			return builder;
		}

		public static WebApplication ConfigureApplication(this WebApplication app)
		{
			app.AddSystemProperties();
			app.AddLoggingServices();
			app.UseLanguageSupport();

			app.UseStaticFiles();
			app.UseRouting();

			return app;
		}

		public static void RunApplication(this WebApplication app)
		{
			ILoggerService loggerService = app.Services.GetRequiredService<ILoggerService>();

			try
			{
				loggerService.LogInfo("Starting web host.");

				app.Run();
			}
			catch (Exception ex)
			{
				loggerService.LogError(ex);
			}
		}

		#region Behind the Scenes

		private static IApplicationBuilder UseLanguageSupport(this IApplicationBuilder app)
		{
			var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
			app.UseRequestLocalization(locOptions.Value);

			return app;
		}

		private static IApplicationBuilder AddSystemProperties(this IApplicationBuilder app)
		{
			using (var scope = app.ApplicationServices.CreateScope())
			{
				EntityPropertySetter systemPropertySetter = scope.ServiceProvider.GetRequiredService<EntityPropertySetter>();
				ILoggerService loggerService = scope.ServiceProvider.GetRequiredService<ILoggerService>();

				systemPropertySetter.SetSystemProperties(loggerService);
			}

			return app;
		}

		#endregion

	}
}
