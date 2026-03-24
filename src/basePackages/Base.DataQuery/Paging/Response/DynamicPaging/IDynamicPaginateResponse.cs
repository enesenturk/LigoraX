namespace Base.DataQuery.Paging.Response.DynamicPaging;

public interface IDynamicPaginateResponse<T>
{
	int From { get; }
	int Index { get; }
	int Size { get; }
	int Count { get; }
	int Pages { get; }
	IList<T> Records { get; }
	bool HasPrevious { get; }
	bool HasNext { get; }
}