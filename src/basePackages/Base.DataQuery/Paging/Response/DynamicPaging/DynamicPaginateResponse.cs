namespace Base.DataQuery.Paging.Response.DynamicPaging;

public class DynamicPaginateResponse<T> : IDynamicPaginateResponse<T>
{
	internal DynamicPaginateResponse(IEnumerable<T> source, int index, int size, int from)
	{
		var enumerable = source as T[] ?? source.ToArray();

		if (from > index)
			throw new ArgumentException($"indexFrom: {from} > pageIndex: {index}, must indexFrom <= pageIndex");

		if (source is IQueryable<T> querable)
		{
			Index = index;
			Size = size;
			From = from;
			Count = querable.Count();
			Pages = (int)Math.Ceiling(Count / (double)Size);

			Records = querable.Skip((Index - From) * Size).Take(Size).ToList();
		}
		else
		{
			Index = index;
			Size = size;
			From = from;

			Count = enumerable.Count();
			Pages = (int)Math.Ceiling(Count / (double)Size);

			Records = enumerable.Skip((Index - From) * Size).Take(Size).ToList();
		}
	}

	internal DynamicPaginateResponse()
	{
		Records = new T[0];
	}

	public int From { get; set; }
	public int Index { get; set; }
	public int Size { get; set; }
	public int Count { get; set; }
	public int Pages { get; set; }
	public IList<T> Records { get; set; }
	public bool HasPrevious => Index - From > 0;
	public bool HasNext => Index - From + 1 < Pages;
}

internal class Paginate<TSource, TResult> : IDynamicPaginateResponse<TResult>
{
	public Paginate(IEnumerable<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter,
					int index, int size, int from)
	{
		var enumerable = source as TSource[] ?? source.ToArray();

		if (from > index) throw new ArgumentException($"From: {from} > Index: {index}, must From <= Index");

		if (source is IQueryable<TSource> queryable)
		{
			Index = index;
			Size = size;
			From = from;
			Count = queryable.Count();
			Pages = (int)Math.Ceiling(Count / (double)Size);

			var items = queryable.Skip((Index - From) * Size).Take(Size).ToArray();

			Records = new List<TResult>(converter(items));
		}
		else
		{
			Index = index;
			Size = size;
			From = from;
			Count = enumerable.Count();
			Pages = (int)Math.Ceiling(Count / (double)Size);

			var items = enumerable.Skip((Index - From) * Size).Take(Size).ToArray();

			Records = new List<TResult>(converter(items));
		}
	}


	public Paginate(IDynamicPaginateResponse<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter)
	{
		Index = source.Index;
		Size = source.Size;
		From = source.From;
		Count = source.Count;
		Pages = source.Pages;

		Records = new List<TResult>(converter(source.Records));
	}

	public int Index { get; }

	public int Size { get; }

	public int Count { get; }

	public int Pages { get; }

	public int From { get; }

	public IList<TResult> Records { get; }

	public bool HasPrevious => Index - From > 0;

	public bool HasNext => Index - From + 1 < Pages;
}

public static class Paginate
{
	public static IDynamicPaginateResponse<T> Empty<T>()
	{
		return new DynamicPaginateResponse<T>();
	}

	public static IDynamicPaginateResponse<TResult> From<TResult, TSource>(IDynamicPaginateResponse<TSource> source,
															Func<IEnumerable<TSource>, IEnumerable<TResult>> converter)
	{
		return new Paginate<TSource, TResult>(source, converter);
	}
}