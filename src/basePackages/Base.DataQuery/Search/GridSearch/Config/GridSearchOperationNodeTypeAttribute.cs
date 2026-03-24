using System.Linq.Expressions;

namespace Base.DataQuery.Search.GridSearch.Config
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    sealed class GridSearchOperationNodeTypeAttribute : Attribute
    {

        public GridSearchOperationNodeTypeAttribute(ExpressionType nodeType, bool isBinary)
        {
            NodeType = nodeType;
            IsBinary = isBinary;
            StringComparisonMethod = StringManipulationMethod = string.Empty;
        }

        public GridSearchOperationNodeTypeAttribute(ExpressionType nodeType,
            bool isBinary,
            string stringComparisonMethod = null,
            string stringManipulationMethod = null)
        {
            NodeType = nodeType;
            IsBinary = isBinary;
            StringComparisonMethod = stringComparisonMethod;
            StringManipulationMethod = stringManipulationMethod;
        }

        public ExpressionType NodeType { get; set; }
        public bool IsBinary { get; set; }
        public string StringComparisonMethod { get; set; }
        public string StringManipulationMethod { get; set; }
    }
}
