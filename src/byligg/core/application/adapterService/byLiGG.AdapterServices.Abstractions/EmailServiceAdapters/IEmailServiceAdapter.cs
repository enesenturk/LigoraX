using byLiGG.AdapterServices.Abstractions.EmailServiceAdapters.Dtos;

namespace byLiGG.AdapterServices.Abstractions.EmailServiceAdapters
{
	public interface IEmailServiceAdapter
	{

		Task SendEmailAsync(EmailDto email);

	}
}