using Base.DataQuery.Search.GridSearch.Config;

namespace Base.DataQuery.Search.GridSearch.Factory
{
    public class GridExpressionOpFactory
    {

        private IOpConfig _config;

        public GridExpressionOpFactory(IOpConfig config)
        {
            _config = config;
        }

        public GridExpressionBehavior GetExpressionType(string value)
        {
            return _config[value];
        }

    }
}
