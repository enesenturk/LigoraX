using LigoraX.AdapterServices.Abstractions.EmailServiceAdapters;
using LigoraX.AdapterServices.EmailServiceAdapters.Dotnet.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LigoraX.AdapterServices
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddAdapterServices(this IServiceCollection services)
		{
			services.AddSingleton<IEmailServiceAdapter, DotnetEmailServiceAdapter>();

			return services;
		}

	}
}
