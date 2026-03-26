using byLiGG.AdapterServices.Abstractions.EmailServiceAdapters;
using byLiGG.AdapterServices.EmailServiceAdapters.Dotnet.Services;
using Microsoft.Extensions.DependencyInjection;

namespace byLiGG.AdapterServices
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
