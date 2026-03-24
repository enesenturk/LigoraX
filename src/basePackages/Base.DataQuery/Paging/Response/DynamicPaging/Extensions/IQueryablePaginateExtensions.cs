using Microsoft.EntityFrameworkCore;

namespace Base.DataQuery.Paging.Response.DynamicPaging.Extensions;

public static class IQueryablePaginateExtensions
{

	public static async Task<IDynamicPaginateResponse<T>> ToPaginateAsync<T>(this IQueryable<T> source, int index, int size, int from = 0,
		CancellationToken cancellationToken = default)
	{
		if (from > index)
			throw new ArgumentException($"From: {from} > Index: {index}, must from <= Index");

		int count = await source.CountAsync(cancellationToken).ConfigureAwait(false);

		List<T> records = await source
			.Skip((index - from) * size).Take(size)
			.ToListAsync(cancellationToken)
			.ConfigureAwait(false);

		DynamicPaginateResponse<T> list = new()
		{
			Index = index,
			Size = size,
			From = from,
			Count = count,
			Records = records,
			Pages = (int)Math.Ceiling(count / (double)size)
		};

		return list;
	}

	public static IDynamicPaginateResponse<T> ToPaginate<T>(this IQueryable<T> source, int index, int size, int from = 0)
	{
		if (from > index) throw new ArgumentException($"From: {from} > Index: {index}, must from <= Index");

		int count = source.Count();

		List<T> records = source
			.Skip((index - from) * size).Take(size)
			.ToList();

		DynamicPaginateResponse<T> list = new()
		{
			Index = index,
			Size = size,
			From = from,
			Count = count,
			Records = records,
			Pages = (int)Math.Ceiling(count / (double)size)
		};

		return list;
	}

}