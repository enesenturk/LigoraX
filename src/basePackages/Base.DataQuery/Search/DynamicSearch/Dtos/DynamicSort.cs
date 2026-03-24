namespace Base.DataQuery.Search.DynamicSearch.Dtos;

public class DynamicSort
{
	public string Field { get; set; }
	public string Dir { get; set; }

	public DynamicSort()
	{
	}

	public DynamicSort(string field, string dir)
	{
		Field = field;
		Dir = dir;
	}
}