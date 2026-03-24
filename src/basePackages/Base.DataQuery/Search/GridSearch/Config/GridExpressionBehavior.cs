using System.Linq.Expressions;

namespace Base.DataQuery.Search.GridSearch.Config
{
	public class GridExpressionBehavior
	{
		public ExpressionType ExpressionType { get; set; }
		public bool IsBinary { get; set; }
		public bool UseMethod { get; set; }
		public string Method { get; set; }
		public bool MethodResultCompareValue { get; set; }
	}
}
