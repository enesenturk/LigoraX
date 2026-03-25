using Base.DataAccess.Repositories.Base.Abstract;
using Base.DataAccess.Repositories.Extentions;
using Base.DataQuery.Paging.Request;
using Base.DataQuery.Paging.Response.DynamicPaging;
using Base.DataQuery.Paging.Response.DynamicPaging.Extensions;
using Base.DataQuery.Search.DynamicSearch.Dtos;
using Base.DataQuery.Search.DynamicSearch.Extentions;
using Base.Entity;
using Base.Exceptions.ExceptionModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Base.DataAccess.Repositories.Base.Concrete
{
	public class EfReadRepositoryBase<T, TContext> : IAsyncReadRepositoryBase<T>, IReadRepositoryBase<T>
		where T : IReadEntity
		where TContext : DbContext, new()
	{

		#region Read

		#region async

		public async Task<int> CountAsync(
			Expression<Func<T, bool>> predicate,
			bool excludeDeleteds = true
			)
		{
			using (var context = new TContext())
			{
				IQueryable<T> queryable = context.Set<T>();

				if (excludeDeleteds)
					queryable = queryable.ExcludeDeleteds();

				return await queryable.CountAsync(predicate);
			}
		}

		public async Task<T> GetAsync(Expression<Func<T, bool>> predicate,
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			bool excludeDeleteds = true
			)
		{
			using (var context = new TContext())
			{
				IQueryable<T> queryable = context.Set<T>();

				if (excludeDeleteds)
					queryable = queryable.ExcludeDeleteds();

				if (include != null)
					queryable = include(queryable);

				return orderBy != null ?
					await orderBy(queryable).FirstOrDefaultAsync(predicate) :
					await queryable.FirstOrDefaultAsync(predicate);
			}
		}

		public async Task<List<T>> GetListAsync(
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
			Expression<Func<T, bool>> predicate = null,
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
			bool enableTracking = true,
			bool excludeDeleteds = true,
			CancellationToken cancellationToken = default)
		{
			using (var context = new TContext())
			{
				IQueryable<T> queryable = context.Set<T>();

				if (excludeDeleteds)
					queryable = queryable.ExcludeDeleteds();

				if (!enableTracking)
					queryable = queryable.AsNoTracking();

				if (include != null)
					queryable = include(queryable);

				if (predicate != null)
					queryable = queryable.Where(predicate);

				return await orderBy(queryable).ToListAsync(cancellationToken);
			}
		}

		public async Task<List<T>> GetPaginateAsync(
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
			PagingRequest pagingRequest,
			Expression<Func<T, bool>> predicate = null,
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
			bool enableTracking = true,
			bool excludeDeleteds = true,
			CancellationToken cancellationToken = default)
		{
			if (pagingRequest is null)
				throw new AbsurdOperationException("GetPaginateAsync must have a not null PagingRequest.");

			using (var context = new TContext())
			{
				IQueryable<T> queryable = context.Set<T>();

				if (excludeDeleteds)
					queryable = queryable.ExcludeDeleteds();

				if (!enableTracking)
					queryable = queryable.AsNoTracking();

				if (include != null)
					queryable = include(queryable);

				if (predicate != null)
					queryable = queryable.Where(predicate);

				return await orderBy(queryable).SkipAndTake(pagingRequest).ToListAsync();
			}
		}

		public async Task<IDynamicPaginateResponse<T>> GetPaginateAsync(
			PagingRequest pagingRequest,
			DynamicQuery dynamic,
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
			bool enableTracking = true,
			bool excludeDeleteds = true,
			CancellationToken cancellationToken = default)
		{
			if (pagingRequest is null)
				throw new AbsurdOperationException("GetPaginateAsync must have a not null PagingRequest.");

			using (var context = new TContext())
			{
				IQueryable<T> queryable = context.Set<T>();

				if (excludeDeleteds)
					queryable = queryable.ExcludeDeleteds();

				queryable = queryable.ToDynamic(dynamic);

				if (!enableTracking)
					queryable = queryable.AsNoTracking();

				if (include != null)
					queryable = include(queryable);

				return await queryable.ToPaginateAsync(pagingRequest.CurrentPage, pagingRequest.TakeCount, 0, cancellationToken);
			}
		}

		public async Task<List<Out>> GroupByAsync<Out, By>(
			Func<IQueryable<Out>, IOrderedQueryable<Out>> orderBy,
			Func<IQueryable<T>, IQueryable<IGrouping<By, T>>> groupBy,
			Func<IQueryable<IGrouping<By, T>>, IQueryable<Out>> select,
			PagingRequest pagingRequest = null,
			Expression<Func<T, bool>> predicate = null,
			bool enableTracking = true,
			bool excludeDeleteds = true,
			CancellationToken cancellationToken = default)
		{
			using (var context = new TContext())
			{
				IQueryable<T> queryable = context.Set<T>();

				if (!enableTracking)
					queryable = queryable.AsNoTracking();

				if (excludeDeleteds)
					queryable = queryable.ExcludeDeleteds();

				if (predicate != null)
					queryable = queryable.Where(predicate);

				IQueryable<IGrouping<By, T>> groupped = groupBy(queryable);
				IQueryable<Out> selected = select(groupped);
				IQueryable<Out> ordered = orderBy(selected);

				return pagingRequest is null ?
					await ordered.ToListAsync() :
					await ordered.SkipAndTake(pagingRequest).ToListAsync();
			}
		}

		public async Task<int> GroupByCountAsync<By>(
			Func<IQueryable<T>, IQueryable<IGrouping<By, T>>> groupBy,
			Expression<Func<T, bool>> predicate = null,
			bool enableTracking = true,
			bool excludeDeleteds = true,
			CancellationToken cancellationToken = default)
		{
			using (var context = new TContext())
			{
				IQueryable<T> queryable = context.Set<T>();

				if (!enableTracking)
					queryable = queryable.AsNoTracking();

				if (excludeDeleteds)
					queryable = queryable.ExcludeDeleteds();

				if (predicate != null)
					queryable = queryable.Where(predicate);

				IQueryable<IGrouping<By, T>> groupped = groupBy(queryable);

				return await groupped.CountAsync();
			}
		}

		#endregion

		#region sync

		public int Count(
			Expression<Func<T, bool>> predicate,
			bool excludeDeleteds = true
			)
		{
			using (var context = new TContext())
			{
				IQueryable<T> queryable = context.Set<T>();

				if (excludeDeleteds)
					queryable = queryable.ExcludeDeleteds();

				return queryable.Count(predicate);
			}
		}

		public T Get<K>(
			Expression<Func<T, bool>> predicate,
			Expression<Func<T, K>> orderBy,
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
			bool excludeDeleteds = true
			)
		{
			using (var context = new TContext())
			{
				IQueryable<T> queryable = context.Set<T>();

				if (excludeDeleteds)
					queryable = queryable.ExcludeDeleteds();

				if (include != null)
					queryable = include(queryable);

				return queryable.OrderByNullsLast(orderBy).FirstOrDefault(predicate);
			}
		}

		public List<T> GetList(
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
			Expression<Func<T, bool>> predicate = null,
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
			bool enableTracking = true,
			bool excludeDeleteds = true
			)
		{
			using (var context = new TContext())
			{
				IQueryable<T> queryable = context.Set<T>();

				if (excludeDeleteds)
					queryable = queryable.ExcludeDeleteds();

				if (!enableTracking)
					queryable = queryable.AsNoTracking();

				if (include != null)
					queryable = include(queryable);

				if (predicate != null)
					queryable = queryable.Where(predicate);

				return orderBy(queryable).ToList();
			}
		}

		public List<T> GetPaginate(
			PagingRequest pagingRequest,
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
			Expression<Func<T, bool>> predicate = null,
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
			bool enableTracking = true,
			bool excludeDeleteds = true
			)
		{
			using (var context = new TContext())
			{
				IQueryable<T> queryable = context.Set<T>();

				if (excludeDeleteds)
					queryable = queryable.ExcludeDeleteds();

				if (!enableTracking)
					queryable = queryable.AsNoTracking();

				if (include != null)
					queryable = include(queryable);

				if (predicate != null)
					queryable = queryable.Where(predicate);

				return orderBy(queryable).SkipAndTake(pagingRequest).ToList();
			}
		}

		public IDynamicPaginateResponse<T> GetPaginate(
			PagingRequest pagingRequest,
			DynamicQuery dynamic,
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
			bool enableTracking = true,
			bool excludeDeleteds = true
			)
		{
			using (var context = new TContext())
			{
				IQueryable<T> queryable = context.Set<T>();

				if (excludeDeleteds)
					queryable = queryable.ExcludeDeleteds();

				queryable = queryable.ToDynamic(dynamic);

				if (!enableTracking)
					queryable = queryable.AsNoTracking();

				if (include != null)
					queryable = include(queryable);

				return queryable.ToPaginate(pagingRequest.CurrentPage, pagingRequest.TakeCount);
			}
		}

		public List<Out> GroupBy<Out, By>(
			Func<IQueryable<Out>, IOrderedQueryable<Out>> orderBy,
			Func<IQueryable<T>, IQueryable<IGrouping<By, T>>> groupBy,
			Func<IQueryable<IGrouping<By, T>>, IQueryable<Out>> select,
			PagingRequest pagingRequest = null,
			Expression<Func<T, bool>> predicate = null,
			bool enableTracking = true,
			bool excludeDeleteds = true,
			CancellationToken cancellationToken = default)
		{
			using (var context = new TContext())
			{
				IQueryable<T> queryable = context.Set<T>();

				if (!enableTracking)
					queryable = queryable.AsNoTracking();

				if (excludeDeleteds)
					queryable = queryable.ExcludeDeleteds();

				if (predicate != null)
					queryable = queryable.Where(predicate);

				IQueryable<IGrouping<By, T>> groupped = groupBy(queryable);
				IQueryable<Out> selected = select(groupped);
				IQueryable<Out> ordered = orderBy(selected);

				return pagingRequest is null ?
					ordered.ToList() :
					ordered.SkipAndTake(pagingRequest).ToList();
			}
		}

		public int GroupByCount<By>(
			Func<IQueryable<T>, IQueryable<IGrouping<By, T>>> groupBy,
			Expression<Func<T, bool>> predicate = null,
			bool enableTracking = true,
			bool excludeDeleteds = true,
			CancellationToken cancellationToken = default)
		{
			using (var context = new TContext())
			{
				IQueryable<T> queryable = context.Set<T>();

				if (!enableTracking)
					queryable = queryable.AsNoTracking();

				if (excludeDeleteds)
					queryable = queryable.ExcludeDeleteds();

				if (predicate != null)
					queryable = queryable.Where(predicate);

				IQueryable<IGrouping<By, T>> groupped = groupBy(queryable);

				return groupped.Count();
			}
		}

		#endregion

		#endregion

	}
}
