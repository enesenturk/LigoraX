using Base.DataQuery.Paging.Request;
using Base.DataQuery.Paging.Response.DynamicPaging;
using Base.DataQuery.Search.DynamicSearch.Dtos;
using Base.Entity;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Base.DataAccess.Repositories.Base.Abstract
{
	public interface IAsyncReadRepositoryBase<T> where T : IReadEntity
	{

		#region Read

		Task<int> CountAsync(
			Expression<Func<T, bool>> predicate,
			bool excludeDeleteds = true
			);

		Task<T> GetAsync(
			Expression<Func<T, bool>> predicate,
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			bool excludeDeleteds = true
			);

		Task<List<T>> GetListAsync(
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
			Expression<Func<T, bool>> predicate = null,
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
			bool enableTracking = true,
			bool excludeDeleteds = true,
			CancellationToken cancellationToken = default
			);

		Task<List<T>> GetPaginateAsync(
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
			PagingRequest pagingRequest,
			Expression<Func<T, bool>> predicate = null,
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
			bool enableTracking = true,
			bool excludeDeleteds = true,
			CancellationToken cancellationToken = default);

		Task<IDynamicPaginateResponse<T>> GetPaginateAsync(
			PagingRequest pagingRequest,
			DynamicQuery dynamic,
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
			bool enableTracking = true,
			bool excludeDeleteds = true,
			CancellationToken cancellationToken = default);

		Task<List<Out>> GroupByAsync<Out, By>(
			Func<IQueryable<Out>, IOrderedQueryable<Out>> orderBy,
			Func<IQueryable<T>, IQueryable<IGrouping<By, T>>> groupBy,
			Func<IQueryable<IGrouping<By, T>>, IQueryable<Out>> select,
			PagingRequest pagingRequest = null,
			Expression<Func<T, bool>> predicate = null,
			bool enableTracking = true,
			bool excludeDeleteds = true,
			CancellationToken cancellationToken = default);

		Task<int> GroupByCountAsync<By>(
			Func<IQueryable<T>, IQueryable<IGrouping<By, T>>> groupBy,
			Expression<Func<T, bool>> predicate = null,
			bool enableTracking = true,
			bool excludeDeleteds = true,
			CancellationToken cancellationToken = default);

		#endregion

	}
}
