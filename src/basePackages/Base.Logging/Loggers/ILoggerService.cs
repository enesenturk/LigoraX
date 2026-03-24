namespace Base.Logging.Loggers
{
	public interface ILoggerService
	{

		void LogInfo(string message);
		void LogError(Exception exception);
		void LogError(string message);

	}
}
