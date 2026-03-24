using Base.Exceptions.ExceptionModels;
using Base.Logging.Loggers;
using LigoraX.Domain.EntityProperty;
using LigoraX.Domain.EntityProperty.SystemProperties;
using LigoraX.DomainServices.Abstractions.Modules.System.Property.Dtos;
using LigoraX.DomainServices.Abstractions.Modules.System.Property.Services;
using System.Reflection;

namespace LigoraX.Application.Utilities.EntityProperty
{
	public class EntityPropertySetter
	{

		#region CTOR

		private readonly ISystemPropertyService _systemPropertyService;

		public EntityPropertySetter(ISystemPropertyService systemPropertyService)
		{
			_systemPropertyService = systemPropertyService;
		}

		#endregion

		public void SetSystemProperties(ILoggerService loggerService)
		{
			List<PropertyTypeBasicDto> propertyTypes = _systemPropertyService.GetPropertyTypes();
			List<SystemPropertyBasicDto> systemProperties = _systemPropertyService.GetSystemProperties();

			SetPropertyTypes(loggerService, propertyTypes);
			SetProperties(propertyTypes, systemProperties);
		}

		#region Behind the Scenes

		private void SetPropertyTypes(ILoggerService loggerService, List<PropertyTypeBasicDto> propertyTypes)
		{
			foreach (PropertyTypeBasicDto propertyType in propertyTypes)
			{
				string propertyName = propertyType.LanguageKey;

				Type systemPropertyType = typeof(SystemPropertyType);

				loggerService.LogInfo($"============={propertyName}======================================");

				PropertyInfo property = systemPropertyType.GetProperty(propertyName);

				if (property == null)
				{
					loggerService.LogInfo($"NOTFOUND:============================={propertyName}======================================");
					throw new AbsurdOperationException("Something went wrong while setting system properties. Please contact to the system admin.");
				}

				property.SetValue(null, propertyType.Id);
			}
		}

		private void SetProperties(List<PropertyTypeBasicDto> propertyTypes, List<SystemPropertyBasicDto> systemProperties)
		{
			foreach (PropertyTypeBasicDto propertyType in propertyTypes)
			{
				string className = propertyType.LanguageKey.Replace("SQL_", "").Replace("_", "");

				Assembly propertyAssembly = typeof(_AssemblyReference).Assembly;
				string propertyNamespace = typeof(_AssemblyReference).Namespace;

				Type classType = propertyAssembly.GetType($"{propertyNamespace}.{className}");

				if (classType is null)
					continue;

				string propertyTypeName = classType.Name;
				int propertyTypeId = Convert.ToInt32(typeof(SystemPropertyType).GetProperty(propertyTypeName).GetValue(null, null));

				PropertyInfo[] classProperties = classType.GetProperties();

				foreach (PropertyInfo classProperty in classProperties)
				{
					string systemPropertyKey = $"SQL_{classProperty.Name}";

					List<SystemPropertyBasicDto> systemPropertiesOfType = systemProperties.Where(x => x.LanguageKey == systemPropertyKey &&
						x.TypeId == propertyTypeId
						).ToList();

					if (systemPropertiesOfType.Count == 0)
						throw new AbsurdOperationException("Something went wrong while setting system properties. Please contact to the system admin.");

					if (systemPropertiesOfType.Count == 1)
						classProperty.SetValue(null, systemPropertiesOfType[0].Id);

					else
						throw new AbsurdOperationException("Something went wrong while setting system properties. Please contact to the system admin.");
				}
			}
		}

		#endregion

	}
}