using Microsoft.Extensions.Logging;

namespace Base.Logging.Loggers.Microsoft
{
	public class MicrosoftConsoleLogger : ILoggerService
	{

		#region CTOR

		private readonly ILogger<MicrosoftConsoleLogger> _logger;

		private static readonly string _marker = "==============================================\n";
		private static readonly string _message = "Message:";
		private static readonly string _exception = "Exception =>";

		public MicrosoftConsoleLogger(ILogger<MicrosoftConsoleLogger> logger)
		{
			_logger = logger;
		}

		#endregion

		public void LogInfo(string message)
		{
			string log = $"{_marker}INFO {_message} {message}";

			_logger.LogInformation(log);
		}

		public void LogError(string message)
		{
			string log = $"{_marker}ERROR {_message} {message}";

			_logger.LogError(log);
		}

		public void LogError(Exception exception)
		{
			string infoLog = exception.Message;
			string exceptionLog = $"{_exception}\n{exception}";

			LogInfo(infoLog);
			LogError(exceptionLog);
		}
	}

}
