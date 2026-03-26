using byLiGG.DomainServices.Abstractions.Modules.System.Property.Dtos;

namespace byLiGG.DomainServices.Abstractions.Modules.System.Property.Services
{
	public interface ISystemPropertyService
	{

		#region Read

		List<PropertyTypeBasicDto> GetPropertyTypes();
		List<SystemPropertyBasicDto> GetSystemProperties();
		List<SystemPropertyBasicDto> GetSystemPropertiesOfType(Guid typeId);

		#endregion

	}
}