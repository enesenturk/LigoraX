using byLiGG.Configuration.AppSettings;
using Microsoft.AspNetCore.Builder;

namespace byLiGG.Configuration.Registration
{
	public static class ServiceRegistration
	{

		public static WebApplicationBuilder AddConfigurations(this WebApplicationBuilder builder)
		{
			builder.Configuration.SetStaticPropertiesOfClass(new List<Type>
			{
				typeof(DataBaseSettings),
				typeof(EmailSettings),
				typeof(JwtSettings),
				typeof(ProjectSettings),
			});

			return builder;
		}

	}
}
