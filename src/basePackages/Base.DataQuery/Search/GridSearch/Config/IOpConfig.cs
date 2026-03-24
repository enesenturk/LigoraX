namespace Base.DataQuery.Search.GridSearch.Config
{
	public interface IOpConfig
	{

		IOpConfig Add(string operation, GridExpressionBehavior expressionType);
		GridExpressionBehavior this[string exprName] { get; }

	}
}
