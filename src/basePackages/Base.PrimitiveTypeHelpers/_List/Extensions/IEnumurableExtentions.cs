namespace Base.PrimitiveTypeHelpers._List.Extensions
{
	public static class IEnumurableExtentions
	{

		public static DateTime GetNearestTo(this IEnumerable<DateTime> Dates, DateTime SpecificDate)
		{
			return (from date in Dates
					orderby (date - SpecificDate).Duration()
					select date)
					.First();
		}

	}
}