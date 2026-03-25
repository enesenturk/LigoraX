using LigoraX.Configuration.AppSettings;
using Microsoft.AspNetCore.Builder;

namespace LigoraX.Configuration.Registration
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
