namespace Base.Logging.Loggers.Serilog
{

	//[Serializable]

	//public class SerilogConsoleLogger : ILoggerService
	//{

	//#region CTOR

	//private static string _marker = "==============================================\n";
	//private static string _message = "Message:";
	//private static string _exception = "Exception =>";

	//public static ILogger _logger { set; get; }

	//#endregion

	//public void LogInfo(string message)
	//{
	//	string log = _marker + "INFO " + _message + message;

	//	_logger.Information(log);
	//}

	//public void LogError(string message)
	//{
	//	string log = _marker + "ERROR "
	//		+ _message + message;

	//	_logger.Error(log);
	//}

	//public void LogError(Exception exception)
	//{
	//	string infoLog = exception.Message;

	//	string exceptionLog = _exception + "\n" + exception;

	//	LogInfo(infoLog);
	//	LogError(exceptionLog);
	//}

	//}
}