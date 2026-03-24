namespace Base.DataQuery.Search.DynamicSearch.Dtos;

public class DynamicQuery
{

	public IEnumerable<DynamicSort> Sort { get; set; }
	public DynamicFilter Filter { get; set; }

	public DynamicQuery()
	{
	}

	public DynamicQuery(IEnumerable<DynamicSort> sort, DynamicFilter filter)
	{
		Sort = sort;
		Filter = filter;
	}

}