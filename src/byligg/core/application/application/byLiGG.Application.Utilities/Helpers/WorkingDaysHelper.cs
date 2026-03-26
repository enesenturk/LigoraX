namespace byLiGG.Application.Utilities.Helpers
{
	public class WorkingDaysHelper
	{

		public static int GetNumberOfWorkingDays(DateTime start, DateTime stop)
		{
			int days = 0;

			while (start <= stop)
			{
				if (start.DayOfWeek != DayOfWeek.Saturday && start.DayOfWeek != DayOfWeek.Sunday)
					++days;

				start = start.AddDays(1);
			}

			return days;
		}

		public static int CountWeekDay(DateTime startDate, DateTime endDate, DayOfWeek weekDay)
		{
			int totalDays = (int)(endDate - startDate).TotalDays;
			int countInWholeWeeks = totalDays / 7;
			int countInRemainings = CountWeekDayInRemaining(weekDay, endDate, totalDays);
			int totalDaysInRange = countInWholeWeeks + countInRemainings;

			return totalDaysInRange;
		}

		#region Behind the Scenes

		private static int CountWeekDayInRemaining(DayOfWeek searchDay, DateTime endDate, int totalDays)
		{
			int remainingDaysCount = totalDays % 7;

			int endIndex = (int)endDate.DayOfWeek;
			int searchIndex = (int)searchDay;

			int differenceCount = GetDifferenceCount(searchIndex, endIndex);

			bool haveSearchDay_InRemaining = remainingDaysCount >= differenceCount;

			if (haveSearchDay_InRemaining)
				return 1;

			return 0;
		}

		private static int GetDifferenceCount(int searchIndex, int endIndex)
		{
			int differenceEndDateAndSearchedDay = endIndex - searchIndex;

			if (differenceEndDateAndSearchedDay < 0)
				differenceEndDateAndSearchedDay += 7;

			return differenceEndDateAndSearchedDay;
		}

		#endregion

	}
}
