using Base.DataQuery.Search.GridSearch.Constants;
using Base.DataQuery.Search.GridSearch.Dtos;
using Base.DataQuery.Search.GridSearch.Factory;
using Base.Entity;
using LinqKit;
using System.Linq.Expressions;
using System.Reflection;

namespace Base.DataQuery.Search.GridSearch.FilterService
{
	public static class ExpressionTreeGenerator
	{

		public static Expression<Func<T, bool>> CreateSearchPredicate<T>(
			GridExpressionOpFactory factory,
			GridQuery gridQuery,
			bool checkIsDeleted = true,
			bool filterByZero = false
			)
		{
			bool isGroupOperationAND = gridQuery.GroupOperation == GridGroupOperation.AND;

			ExpressionStarter<T> predicate = isGroupOperationAND ?
			PredicateBuilder.New<T>(true) :
			PredicateBuilder.New<T>(false);

			// Check is has any filter
			if (
				gridQuery.Filters == null ||
				gridQuery.Filters.Count == 0 ||
				(gridQuery.Filters.Count == 1 && IsSkip(gridQuery.Filters[0], filterByZero))
				)
			{
				if (checkIsDeleted)
				{
					return IsDeletedFilter<T>();
				}
				else
				{
					return x => 5 > 1;
				}
			}

			if (checkIsDeleted && isGroupOperationAND)
			{
				predicate = predicate.And(IsDeletedFilter<T>());
			}

			foreach (GridFilter rule in gridQuery.Filters)
			{
				bool skip = IsSkip(rule, filterByZero);

				if (skip)
					continue;

				var operationAttribute = factory.GetExpressionType(rule.Operation);

				ParameterExpression lhsParam = Expression.Parameter(typeof(T));
				Expression lhs = CreateExpression(lhsParam, rule.Field);

				Expression rhs;

				Type lhsType = lhs.Type;

				Type nonNullableType = Nullable.GetUnderlyingType(lhsType);
				if (nonNullableType != null)
				{
					// it is a nullable type
					rhs = Expression.Constant(Convert.ChangeType(rule.FieldData.ToLower(), nonNullableType), lhsType);
				}
				else
				{
					rhs = Expression.Constant(Convert.ChangeType(rule.FieldData.ToLower(), lhsType));
					//Expression rhs = Expression.Constant(Convert.ChangeType(rule.FieldData, pi.PropertyType));
				}

				Expression theOperation;
				if (operationAttribute.UseMethod)
				{
					lhs = Expression.Call(Expression.Call(lhs, GridOperation.ToLower, null), operationAttribute.Method, null, rhs);
					//lhs = Expression.Call(lhs, operationAttribute.Method, null, rhs);
				}

				if (operationAttribute.IsBinary)
				{
					theOperation = Expression.MakeBinary(operationAttribute.ExpressionType, lhs, rhs);
				}
				else  // need to fix this
				{
					theOperation = Expression.MakeBinary(operationAttribute.ExpressionType, lhs,
						Expression.Constant(operationAttribute.MethodResultCompareValue));
				}

				var theLambda = Expression.Lambda<Func<T, bool>>(theOperation, lhsParam);

				if (isGroupOperationAND)
				{
					predicate = predicate.And(theLambda);
				}
				else
				{
					predicate = predicate.Or(theLambda);
				}
			}

			return predicate;
		}


		#region Behing the Scenes

		private static bool IsSkip(GridFilter rule, bool filterByZero)
		{
			bool skip = filterByZero
				? rule.FieldData == null || rule.FieldData == "null" || rule.FieldData == ""
				: rule.FieldData == null || rule.FieldData == "null" || rule.FieldData == "" || rule.FieldData == "0";

			return skip;
		}

		private static Expression<Func<T, bool>> IsDeletedFilter<T>()
		{
			PropertyInfo pi = typeof(T).GetProperty(nameof(IMutationEntity.is_deleted));
			ParameterExpression lhsParam = Expression.Parameter(typeof(T));
			Expression lhs = Expression.Property(lhsParam, pi);

			Expression rhs = Expression.Constant(true);

			Expression theOperation = Expression.MakeBinary(ExpressionType.NotEqual, lhs, rhs);

			return Expression.Lambda<Func<T, bool>>(theOperation, lhsParam);
		}

		private static Expression CreateExpression(ParameterExpression parameter, string propertyName)
		{
			Expression body = parameter;

			foreach (var member in propertyName.Split('.'))
			{
				body = Expression.PropertyOrField(body, member);
			}

			return body;
		}

		#endregion

	}
}
