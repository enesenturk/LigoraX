namespace Base.Constants.Formats
{
	public class DateTimeFormats
	{

		public static string Get_Sql_DateTimeFormat(bool includeMilliseconds = false) => includeMilliseconds ? "yyyy-MM-dd HH:mm:ss.fff" : "yyyy-MM-dd HH:mm:ss";
		public static string Get_Iso8601_DateTimeFormat(bool includeMilliseconds = false) => includeMilliseconds ? "yyyy-MM-ddTHH:mm:ss.fff" : "yyyy-MM-ddTHH:mm:ss";
		public static string GetCompact_Iso8601_DateTimeFormat(bool includeSeconds = false) => includeSeconds ? "yyyyMMddTHHmmss" : "yyyyMMddTHHmm";

	}
}
