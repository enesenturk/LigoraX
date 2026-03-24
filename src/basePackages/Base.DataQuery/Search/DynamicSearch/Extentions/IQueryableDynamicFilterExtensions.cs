using Base.DataQuery.Search.DynamicSearch.Dtos;
using System.Linq.Dynamic.Core;
using System.Text;

namespace Base.DataQuery.Search.DynamicSearch.Extentions;

public static class IQueryableDynamicFilterExtensions
{

    public static IQueryable<T> ToDynamic<T>(this IQueryable<T> query, DynamicQuery dynamic)
    {
        if (dynamic is null)
            return query;

        if (dynamic.Filter is not null)
            query = Filter(query, dynamic.Filter);

        if (dynamic.Sort is not null && dynamic.Sort.Any())
            query = Sort(query, dynamic.Sort);

        return query;
    }

    #region Behind the Scenes

    private static readonly IDictionary<string, string>
        Operators = new Dictionary<string, string>
        {
            { "eq", "=" },
            { "neq", "!=" },
            { "lt", "<" },
            { "lte", "<=" },
            { "gt", ">" },
            { "gte", ">=" },
            { "isnull", "== null" },
            { "isnotnull", "!= null" },
            { "startswith", "StartsWith" },
            { "endswith", "EndsWith" },
            { "contains", "Contains" },
            { "doesnotcontain", "Contains" }
        };

    private static IQueryable<T> Filter<T>(IQueryable<T> queryable, DynamicFilter filter)
    {
        IList<DynamicFilter> filters = GetAllFilters(filter);

        string[] values = filters.Select(f => f.Value).ToArray();

        string where = Transform(filter, filters);

        queryable = queryable.Where(where, values);

        return queryable;
    }

    private static IQueryable<T> Sort<T>(IQueryable<T> queryable, IEnumerable<DynamicSort> sort)
    {
        if (sort.Any())
        {
            string ordering = string.Join(",", sort.Select(s => $"{s.Field} {s.Dir}"));

            return queryable.OrderBy(ordering);
        }

        return queryable;
    }

    private static IList<DynamicFilter> GetAllFilters(DynamicFilter filter)
    {
        List<DynamicFilter> filters = new List<DynamicFilter>();

        GetFilters(filter, filters);

        return filters;
    }

    private static void GetFilters(DynamicFilter filter, IList<DynamicFilter> filters)
    {
        filters.Add(filter);

        if (filter.Filters is not null && filter.Filters.Any())
        {
            foreach (DynamicFilter item in filter.Filters)
            {
                GetFilters(item, filters);
            }
        }
    }

    private static string Transform(DynamicFilter filter, IList<DynamicFilter> filters)
    {
        int index = filters.IndexOf(filter);

        string comparison = Operators[filter.Operator];

        StringBuilder where = new StringBuilder();

        if (!string.IsNullOrEmpty(filter.Value))
        {
            if (filter.Operator == "doesnotcontain")
            {
                where.Append($"(!np({filter.Field}).{comparison}(@{index}))");
            }
            else if (comparison == "StartsWith" || comparison == "EndsWith" || comparison == "Contains")
            {
                where.Append($"(np({filter.Field}).{comparison}(@{index}))");
            }
            else
            {
                where.Append($"np({filter.Field}) {comparison} @{index}");
            }
        }
        else if (filter.Operator == "isnull" || filter.Operator == "isnotnull")
        {
            where.Append($"np({filter.Field}) {comparison}");
        }

        if (filter.Logic is not null && filter.Filters is not null && filter.Filters.Any())
        {
            return $"{where} {filter.Logic} ({string.Join($" {filter.Logic} ", filter.Filters.Select(f => Transform(f, filters)).ToArray())})";
        }

        return where.ToString();
    }

    #endregion

}