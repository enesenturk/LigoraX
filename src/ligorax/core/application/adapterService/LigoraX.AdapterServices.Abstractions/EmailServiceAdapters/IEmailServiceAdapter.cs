using LigoraX.AdapterServices.Abstractions.EmailServiceAdapters.Dtos;

namespace LigoraX.AdapterServices.Abstractions.EmailServiceAdapters
{
	public interface IEmailServiceAdapter
	{

		Task SendEmailAsync(EmailDto email);

	}
}