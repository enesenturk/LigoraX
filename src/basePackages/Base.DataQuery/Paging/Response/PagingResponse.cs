namespace Base.DataQuery.Paging.Response
{
	public class PagingResponse<T>
	{

		public List<T> Records { get; set; }
		public int TotalRecordCount { get; set; }

	}
}
