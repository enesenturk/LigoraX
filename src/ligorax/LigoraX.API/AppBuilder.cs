using Base.Caching;
using Base.Cookie;
using Base.Factory;
using Base.Logging;
using Base.Logging.Loggers;
using LigoraX.AdapterServices;
using LigoraX.Application.UseCases;
using LigoraX.Application.Utilities;
using LigoraX.Application.Utilities.EntityProperty;
using LigoraX.AuthorizationServices;
using LigoraX.Configuration.AppSettings;
using LigoraX.Configuration.Registration;
using LigoraX.DomainServices;
using LigoraX.Persistence;
using LigoraX.Presentation;
using LigoraX.Presentation.Constants.CookieConstants;
using LigoraX.Presentation.Filters;
using LigoraX.Presentation.Formatters;
using LigoraX.Presentation.Pipelines;
using LigoraX.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace LigoraX.API
{
	public static class AppBuilder
	{

		public static WebApplicationBuilder BuildApplication(this WebApplicationBuilder builder)
		{
			builder.Services.AddHttpContextAccessor();
			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo { Title = "LigoraX API", Version = "v1" });

				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					Scheme = "bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
				});

				options.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
						},
						Array.Empty<string>()
					}
				});
			});

			builder.AddConfigurations();
			builder.Services.AddJwtAuthentication();
			builder.Services.AddMvcSettings();
			builder.Services.AddSetAuthorizationBehavior();
			builder.Services.AddAdapterServices();
			builder.Services.AddAuthorizationServices();
			builder.Services.AddUseCaseServices();
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
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI(options =>
				{
					options.SwaggerEndpoint("/swagger/v1/swagger.json", "LigoraX API v1");
					options.RoutePrefix = string.Empty;
				});
			}

			app.AddSystemProperties();
			app.AddLoggingServices();
			app.UseLanguageSupport();

			app.UseStaticFiles();
			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();

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

		private static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
		{
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = JwtSettings.Issuer,
						ValidAudience = JwtSettings.Audience,
						IssuerSigningKey = new SymmetricSecurityKey(
							Encoding.UTF8.GetBytes(JwtSettings.SecretKey)),
					};
				});

			services.AddAuthorization();

			return services;
		}

		private static IServiceCollection AddMvcSettings(this IServiceCollection services)
		{
			services.AddMvc(options =>
			{
				options.MaxModelBindingCollectionSize = int.MaxValue;

				options.Filters.Add(typeof(ExceptionHandler));
				options.Filters.Add(typeof(TrackExecution));
			})
			.AddViewLocalization()
			.AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.PropertyNamingPolicy = null;
				options.JsonSerializerOptions.DictionaryKeyPolicy = null;
			});

			return services;
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

		public static IServiceCollection AddSetAuthorizationBehavior(this IServiceCollection services)
		{
			services.AddScoped(typeof(IPipelineBehavior<,>), typeof(SetAuthContextBehaviour<,>));

			return services;
		}

		private static IApplicationBuilder UseLanguageSupport(this IApplicationBuilder app)
		{
			var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
			app.UseRequestLocalization(locOptions.Value);

			return app;
		}

		#endregion

	}
}