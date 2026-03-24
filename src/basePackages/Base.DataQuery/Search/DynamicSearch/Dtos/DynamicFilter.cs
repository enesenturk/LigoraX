namespace Base.DataQuery.Search.DynamicSearch.Dtos;

public class DynamicFilter
{
	public string Field { get; set; }
	public string Operator { get; set; }
	public string Value { get; set; }
	public string Logic { get; set; }
	public IEnumerable<DynamicFilter> Filters { get; set; }

	public DynamicFilter()
	{
	}

	public DynamicFilter(string field, string @operator, string value, string logic, IEnumerable<DynamicFilter> filters) : this()
	{
		Field = field;
		Operator = @operator;
		Value = value;
		Logic = logic;
		Filters = filters;
	}
}