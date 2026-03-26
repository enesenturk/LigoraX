using Base.Exceptions.ExceptionModels;
using byLiGG.Domain.EntityProperty.SystemProperties;
using byLiGG.Domain.Language.Helpers;
using System.Reflection;

namespace byLiGG.Application.Utilities.EntityProperty
{
	public class EntityPropertyLanguageMapper
	{

		public static string GetEntityPropertyUiMessage<T>(Guid value)
		{
			Type entityPropertyType = typeof(T);

			string propertyNamespace = typeof(_AssemblyReference).Namespace;

			if (entityPropertyType.Namespace != propertyNamespace)
				throw new AbsurdOperationException($"Type {entityPropertyType.Name} is not an EntityProperty.");

			string entityPropertyName = GetEntityPropertyName(entityPropertyType, value);

			return uiTextHelper.GetUiMessage(entityPropertyName);
		}

		#region Behind the Scenes

		private static string GetEntityPropertyName(Type entityPropertyType, Guid value)
		{
			PropertyInfo[] staticProperties = entityPropertyType.GetProperties(
				BindingFlags.Public |
				BindingFlags.Static |
				BindingFlags.FlattenHierarchy
				);

			foreach (PropertyInfo property in staticProperties)
			{
				if (property.PropertyType == typeof(Guid))
				{
					Guid fieldValue = (Guid)property.GetValue(null);

					if (fieldValue == value)
						return property.Name;
				}
				else
				{
					throw new AbsurdOperationException($"EntityProperty must return Guid.");
				}
			}

			throw new AbsurdOperationException($"value: {value} could not be found in {entityPropertyType.Name}.");
		}

		#endregion

	}
}
