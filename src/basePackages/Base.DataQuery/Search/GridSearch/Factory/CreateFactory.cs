using Base.DataQuery.Search.GridSearch.Config;
using Base.DataQuery.Search.GridSearch.Constants;
using System.Linq.Expressions;

namespace Base.DataQuery.Search.GridSearch.Factory
{
	public static class CreateFactory
	{

		public static GridExpressionOpFactory CreatePredicateFactory()
		{
			IOpConfig _config = new OpConfig()

			.Add(GridOperation.Equals, new GridExpressionBehavior
			{
				IsBinary = true,
				ExpressionType = ExpressionType.Equal
			})
			.Add(GridOperation.NotEquals, new GridExpressionBehavior
			{
				IsBinary = true,
				ExpressionType = ExpressionType.NotEqual
			})
			.Add(GridOperation.GreaterThan, new GridExpressionBehavior
			{
				IsBinary = true,
				ExpressionType = ExpressionType.GreaterThan
			})
			.Add(GridOperation.GreaterThanOrEqual, new GridExpressionBehavior
			{
				IsBinary = true,
				ExpressionType = ExpressionType.GreaterThanOrEqual
			})
			.Add(GridOperation.LessThan, new GridExpressionBehavior
			{
				IsBinary = true,
				ExpressionType = ExpressionType.LessThan
			})
			.Add(GridOperation.LessThanOrEqual, new GridExpressionBehavior
			{
				IsBinary = true,
				ExpressionType = ExpressionType.LessThanOrEqual
			})
			.Add(GridOperation.StartsWith, new GridExpressionBehavior
			{
				IsBinary = false,
				MethodResultCompareValue = true,
				ExpressionType = ExpressionType.Equal,
				UseMethod = true,
				Method = GridOperation.StartsWith
			})
			.Add(GridOperation.NotStartsWith, new GridExpressionBehavior
			{
				IsBinary = false,
				MethodResultCompareValue = false,
				ExpressionType = ExpressionType.Equal,
				UseMethod = true,
				Method = GridOperation.NotStartsWith
			})
			.Add(GridOperation.EndsWith, new GridExpressionBehavior
			{
				IsBinary = false,
				MethodResultCompareValue = true,
				ExpressionType = ExpressionType.Equal,
				UseMethod = true,
				Method = GridOperation.EndsWith
			})
			.Add(GridOperation.NotEndsWith, new GridExpressionBehavior
			{
				IsBinary = false,
				MethodResultCompareValue = false,
				ExpressionType = ExpressionType.Equal,
				UseMethod = true,
				Method = GridOperation.NotEndsWith
			})
			.Add(GridOperation.Contains, new GridExpressionBehavior
			{
				IsBinary = false,
				MethodResultCompareValue = true,
				ExpressionType = ExpressionType.Equal,
				UseMethod = true,
				Method = GridOperation.Contains
			})
			.Add(GridOperation.NotContains, new GridExpressionBehavior
			{
				IsBinary = false,
				MethodResultCompareValue = false,
				ExpressionType = ExpressionType.Equal,
				UseMethod = true,
				Method = GridOperation.NotContains
			});

			GridExpressionOpFactory _factory = new GridExpressionOpFactory(_config);

			return _factory;
		}

	}
}
