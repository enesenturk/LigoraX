using Base.DataQuery.Paging;
using Base.DataQuery.Paging.Request;
using Base.DataQuery.Search.GridSearch.Constants;
using Base.DataQuery.Search.GridSearch.Dtos;
using Base.Entity;
using Base.Exceptions.ExceptionModels;
using Base.PrimitiveTypeHelpers._DateTime.Entensions;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace Base.DataQuery.Search.GridSearch.FilterService
{
	public class QueryGenerator
	{

		public static string CreateRawQueryPredicate<T>(GridQuery gridQuery, bool filterByIsDeleted = true)
		{
			Type tableType = typeof(T);

			gridQuery = GetSQLDateTimeConversionValue<T>(gridQuery, tableType);

			string table = typeof(T).Name;
			string isActive = nameof(IMutationEntity.is_deleted);

			string query = "";

			bool dontHaveOtherFilters = gridQuery == null ||
				gridQuery.Filters.Where(x => !(x.FieldData == "null" || x.FieldData == null || x.FieldData == "" || x.FieldData == "0")).Count() == 0;

			if (filterByIsDeleted)
				query += $"({table}.{isActive} != true) ";

			if (dontHaveOtherFilters)
				return query;

			if (filterByIsDeleted)
				query += "AND ";

			string operation = gridQuery.GroupOperation.ToString();
			string subQuery = "( ";
			int len = gridQuery.Filters.Count;

			for (int i = 0; i < len; i++)
			{
				GridFilter rule = gridQuery.Filters[i];

				string field = rule.Field;
				string value = rule.FieldData;

				if (value == "null" || value == null || value == "" || value == "0")
				{
					if (i == len - 1)
					{
						subQuery = subQuery.Remove(subQuery.LastIndexOf(operation));
						subQuery += ") ";
					}
					continue;
				}

				string comparison = GetQueryOperation<T>(field, rule.Operation, value);

				subQuery += $"{table}.{field} {comparison} ";

				if (i == len - 1)
					subQuery += ") ";
				else
					subQuery += $"{operation} ";
			}

			return query + subQuery;
		}

		public static void AddRawQueryPagination(ref string query, PagingRequest pagingRequest)
		{
			if (pagingRequest is null)
			{
				query += "; ";
			}
			else
			{
				query +=
					$"OFFSET {pagingRequest.GetSkipCount()} ROWS " +
					$"FETCH NEXT {pagingRequest.TakeCount} ROWS ONLY;";
			}
		}

		#region Behind the Scenes

		private static string GetQueryOperation<T>(string field, string operation, string value)
		{
			value = GetSqlValue<T>(field, value);

			return operation switch
			{
				string op when op == GridOperation.Equals => $"= {value}",
				string op when op == GridOperation.NotEquals => $"!= {value}",
				string op when op == GridOperation.GreaterThan => $"> {value}",
				string op when op == GridOperation.GreaterThanOrEqual => $">= {value}",
				string op when op == GridOperation.LessThan => $"< {value}",
				string op when op == GridOperation.LessThanOrEqual => $"<= {value}",
				string op when op == GridOperation.StartsWith => $"ILIKE '{value}%'",
				string op when op == GridOperation.NotStartsWith => $"NOT ILIKE '{value}%'",
				string op when op == GridOperation.EndsWith => $"ILIKE '%{value}'",
				string op when op == GridOperation.NotEndsWith => $"NOT ILIKE '%{value}'",
				string op when op == GridOperation.Contains => $"ILIKE '%{value}%'",
				string op when op == GridOperation.NotContains => $"NOT ILIKE '%{value}%'",
				_ => throw new AbsurdOperationException("Unexpected operator!")
			};
		}

		private static Type GetType<T>(string field)
		{
			Type fieldType = typeof(T).GetProperty(field).PropertyType;
			Type nonNullableType = Nullable.GetUnderlyingType(fieldType);

			return nonNullableType ?? fieldType;
		}

		private static string GetSqlValue<T>(string field, string value)
		{
			string fieldType = GetType<T>(field).Name;

			switch (fieldType)
			{
				case "string":
					return $"'{value}'";

				case "DateTime":
					return $"'{value}'";

				case "Boolean":
					if (value == "true")
						return "true";
					else
						return "false";

				default:
					return value;
			}

		}

		private static GridQuery GetSQLDateTimeConversionValue<T>(GridQuery gridQuery, Type tableType)
		{
			CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;

			foreach (GridFilter x in gridQuery.Filters)
			{
				PropertyInfo propertyInfo = tableType.GetProperty(x.Field);

				Type propertyType = propertyInfo.PropertyType;

				if (string.IsNullOrEmpty(x.FieldData))
					continue;

				if (propertyType == typeof(DateTime?) || propertyType == typeof(DateTime))
				{
					DateTime tst = DateTime.Parse(x.FieldData, cultureInfo);
					x.FieldData = tst.ToSQLDateTime();
				}
			}

			return gridQuery;
		}

		#endregion

	}
}
