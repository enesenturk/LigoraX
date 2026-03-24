using Base.DataQuery.Paging;
using Base.DataQuery.Paging.Request;
using Base.Entity;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;

namespace Base.DataAccess.Repositories.Extentions
{
	public static class IQueryableExtentions
	{

		public static IQueryable<T> ExcludeDeleteds<T>(this IQueryable<T> source) where T : IReadEntity
		{
			return source.Where(x => x.is_deleted == false);
		}

		public static IQueryable<T> SkipAndTake<T>(this IQueryable<T> source, PagingRequest pagingRequest)
		{
			return source.Skip(pagingRequest.GetSkipCount()).Take(pagingRequest.TakeCount);
		}

		public static IOrderedQueryable<T> OrderByNullsLast<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> keySelector)
		{
			string filterMemberName = GetFilterMemberName(keySelector);

			return source.OrderBy($"{filterMemberName} == @0", [null]).ThenBy(keySelector);
		}

		public static IOrderedQueryable<T> OrderByDescendingNullsLast<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> keySelector)
		{
			string filterMemberName = GetFilterMemberName(keySelector);

			return source.OrderBy($"{filterMemberName} == @0", [null]).ThenByDescending(keySelector);
		}

		#region Behind the Scenes

		private static string GetFilterMemberName<T, TKey>(Expression<Func<T, TKey>> keySelector)
		{
			MemberInfo filterMember = (keySelector.Body as MemberExpression).Member;
			string filterMemberName = filterMember.Name;

			if (filterMember.ReflectedType != typeof(T))
			{
				string expressionString = keySelector.ToString();
				filterMemberName = expressionString.Substring(expressionString.IndexOf('.') + 1);
			}

			return filterMemberName;
		}

		#endregion

	}
}
