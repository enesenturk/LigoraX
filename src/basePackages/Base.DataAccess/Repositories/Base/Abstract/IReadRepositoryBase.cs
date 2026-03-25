using Base.DataQuery.Paging.Request;
using Base.DataQuery.Paging.Response.DynamicPaging;
using Base.DataQuery.Search.DynamicSearch.Dtos;
using Base.Entity;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Base.DataAccess.Repositories.Base.Abstract
{
	public interface IReadRepositoryBase<T> where T : IReadEntity
	{

		#region Read

		int Count(
			Expression<Func<T, bool>> predicate,
			bool excludeDeleteds = true
			);

		T Get<K>(
			Expression<Func<T, bool>> predicate,
			Expression<Func<T, K>> orderBy,
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
			bool excludeDeleteds = true
			);

		List<T> GetList(
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
			Expression<Func<T, bool>> predicate = null,
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
			bool enableTracking = true,
			bool excludeDeleteds = true
			);

		List<T> GetPaginate(
			PagingRequest pagingRequest,
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
			Expression<Func<T, bool>> predicate = null,
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
			bool enableTracking = true,
			bool excludeDeleteds = true
			);

		IDynamicPaginateResponse<T> GetPaginate(
			PagingRequest pagingRequest,
			DynamicQuery dynamic,
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
			bool enableTracking = true,
			bool excludeDeleteds = true
			);

		List<Out> GroupBy<Out, By>(
			Func<IQueryable<Out>, IOrderedQueryable<Out>> orderBy,
			Func<IQueryable<T>, IQueryable<IGrouping<By, T>>> groupBy,
			Func<IQueryable<IGrouping<By, T>>, IQueryable<Out>> select,
			PagingRequest pagingRequest = null,
			Expression<Func<T, bool>> predicate = null,
			bool enableTracking = true,
			bool excludeDeleteds = true,
			CancellationToken cancellationToken = default);

		int GroupByCount<By>(
			Func<IQueryable<T>, IQueryable<IGrouping<By, T>>> groupBy,
			Expression<Func<T, bool>> predicate = null,
			bool enableTracking = true,
			bool excludeDeleteds = true,
			CancellationToken cancellationToken = default);

		#endregion

	}
}
