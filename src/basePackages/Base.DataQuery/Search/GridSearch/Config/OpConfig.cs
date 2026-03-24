namespace Base.DataQuery.Search.GridSearch.Config
{
	public class OpConfig : IOpConfig
	{
		private Dictionary<string, GridExpressionBehavior> _expressionTypeMap;

		public OpConfig()
		{
			_expressionTypeMap = new Dictionary<string, GridExpressionBehavior>();
		}

		public IOpConfig Add(string operation, GridExpressionBehavior expressionType)
		{
			_expressionTypeMap.Add(operation, expressionType);
			return this;
		}

		public GridExpressionBehavior this[string exprName]
		{
			get
			{
				if (_expressionTypeMap.ContainsKey(exprName))
				{
					return _expressionTypeMap[exprName];
				}
				else
				{
					return null;
				}
			}
		}
	}
}
