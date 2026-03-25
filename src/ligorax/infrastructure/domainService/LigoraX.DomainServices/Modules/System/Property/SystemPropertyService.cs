using AutoMapper;
using LigoraX.Domain.Entities;
using LigoraX.DomainServices.Abstractions.Modules.System.Property.Dtos;
using LigoraX.DomainServices.Abstractions.Modules.System.Property.Services;
using LigoraX.Repositories.Abstractions.Modules;

namespace LigoraX.DomainServices.Modules.System.Property
{
	public class SystemPropertyService : ISystemPropertyService
	{

		#region CTOR

		private readonly ISystemPropertyTypeRepository _systemPropertyTypeRepository;
		private readonly ISystemPropertyRepository _systemPropertyRepository;
		private readonly IMapper _mapper;

		public SystemPropertyService(
			ISystemPropertyTypeRepository systemPropertyTypeRepository,
			ISystemPropertyRepository systemPropertyRepository,
			IMapper mapper)
		{
			_systemPropertyTypeRepository = systemPropertyTypeRepository;
			_systemPropertyRepository = systemPropertyRepository;
			_mapper = mapper;
		}

		#endregion

		#region Read

		public List<PropertyTypeBasicDto> GetPropertyTypes()
		{
			List<t_system_property_type> propertyTypes = _systemPropertyTypeRepository.GetList(
				orderBy: o => o.OrderBy(x => x.sort_order));

			return _mapper.Map<List<PropertyTypeBasicDto>>(propertyTypes);
		}

		public List<SystemPropertyBasicDto> GetSystemProperties()
		{
			List<t_system_property> systemProperties = _systemPropertyRepository.GetList(
				orderBy: o => o.OrderBy(x => x.sort_order));

			return _mapper.Map<List<SystemPropertyBasicDto>>(systemProperties);
		}

		public List<SystemPropertyBasicDto> GetSystemPropertiesOfType(Guid typeId)
		{
			List<t_system_property> systemProperties = _systemPropertyRepository.GetList(
				orderBy: o => o.OrderBy(x => x.sort_order),
				predicate: x => x.t_system_property_type_id == typeId);

			return _mapper.Map<List<SystemPropertyBasicDto>>(systemProperties);
		}

		#endregion

	}
}