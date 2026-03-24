using LigoraX.AdapterServices.Abstractions.EmailServiceAdapters;
using LigoraX.AdapterServices.Abstractions.EmailServiceAdapters.Dtos;
using LigoraX.Configuration.AppSettings;
using System.Net;
using System.Net.Mail;

namespace LigoraX.AdapterServices.EmailServiceAdapters.Dotnet.Services
{
	public class DotnetEmailServiceAdapter : IEmailServiceAdapter
	{

		public async Task SendEmailAsync(EmailDto email)
		{
			string clientUrl = EmailSettings.ClientUrl;
			int clientPort = EmailSettings.ClientPort;

			string senderEmailAddress = EmailSettings.SenderEmailAddress;
			string senderEmailPassword = EmailSettings.SenderEmailPassword;

			using (MailMessage mail = new MailMessage(senderEmailAddress, email.SendTo, email.Subject, email.Body))
			{
				mail.IsBodyHtml = true;

				using (SmtpClient smtpClient = new SmtpClient(clientUrl, clientPort))
				{
					smtpClient.UseDefaultCredentials = false;
					smtpClient.Credentials = new NetworkCredential(senderEmailAddress, senderEmailPassword);
					smtpClient.EnableSsl = true;

					await smtpClient.SendMailAsync(mail);
				}
			}
		}

	}
}
