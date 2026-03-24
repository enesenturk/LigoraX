using LigoraX.DomainServices.Abstractions.Modules.System.Property.Dtos;

namespace LigoraX.DomainServices.Abstractions.Modules.System.Property.Services
{
	public interface ISystemPropertyService
	{

		#region Read

		List<PropertyTypeBasicDto> GetPropertyTypes();
		List<SystemPropertyBasicDto> GetSystemProperties();
		List<SystemPropertyBasicDto> GetSystemPropertiesOfType(int typeId);

		#endregion

	}
}
