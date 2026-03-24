using Base.DataQuery.Paging.Request;
using Base.Entity;

namespace Base.DataQuery.Paging
{
	public static class PagingExtensions
	{

		public static int GetTotalRecordCount<T>(this List<T> records) where T : IPagingEntity
		{
			var record = records.FirstOrDefault();

			return record == null
				? 0
				: record.total_row_count;
		}

		public static int GetSkipCount(this PagingRequest pagingRequest)
		{
			return (pagingRequest.CurrentPage - 1) * pagingRequest.TakeCount;
		}

	}
}
