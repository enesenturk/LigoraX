using Base.Exceptions.ExceptionModels;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace LigoraX.Configuration.Registration
{
	public static class RegistrationHelper
	{

		public static void SetStaticPropertiesOfClass(this IConfiguration configuration, List<Type> classTypes)
		{
			foreach (var classType in classTypes)
			{
				var props = classType.GetProperties(BindingFlags.Static | BindingFlags.Public);

				foreach (var prop in props)
				{
					if (prop.CanWrite)
					{
						string configKey = $"{classType.Name}_{prop.Name}";
						string configValue = configuration.GetSection(configKey).Value;

						if (!string.IsNullOrEmpty(configValue))
						{
							try
							{
								object typedValue = prop.PropertyType == typeof(Guid) ?
										Guid.Parse(configValue) :
										Convert.ChangeType(configValue, prop.PropertyType);

								prop.SetValue(null, typedValue);
							}
							catch (Exception ex)
							{
								throw new AbsurdOperationException($"Failed to set property '{configKey}' with value from configuration: {ex.Message}");
							}
						}
						else
						{
							throw new AbsurdOperationException($"Failed to set property '{configKey}'. configValue is not found.");
						}
					}
				}
			}
		}

	}
}
