using Base.Constants.Formats;

namespace Base.PrimitiveTypeHelpers._DateTime.Entensions
{
	public static class DateTimeToStringExtensions
	{

		public static string ToSQLDateTime(this DateTime dateTime, bool includeMilliseconds = true)
		{
			return dateTime.ToString(DateTimeFormats.Get_Sql_DateTimeFormat(includeMilliseconds));
		}

		public static string ToSQLDateTimeWithT(this DateTime dateTime, bool includeMilliseconds = true)
		{
			return dateTime.ToString(DateTimeFormats.Get_Iso8601_DateTimeFormat(includeMilliseconds));
		}

		public static string ToCompactISO8601(this DateTime dateTime, bool includeSeconds = false)
		{
			return dateTime.ToString(DateTimeFormats.GetCompact_Iso8601_DateTimeFormat(includeSeconds));
		}

	}
}
