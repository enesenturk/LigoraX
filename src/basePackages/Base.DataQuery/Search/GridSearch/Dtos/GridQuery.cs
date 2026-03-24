
using Base.DataQuery.Search.GridSearch.Constants;

namespace Base.DataQuery.Search.GridSearch.Dtos
{
	public class GridQuery
	{

		public string GroupOperation { get; set; } = GridGroupOperation.AND;
		public List<GridFilter> Filters { get; set; } = new List<GridFilter>();
	}
}
