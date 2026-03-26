using AutoMapper;
using byLiGG.Domain.Entities;
using byLiGG.DomainServices.Abstractions.Modules.System.Property.Dtos;

namespace byLiGG.DomainServices.Modules.System.Property.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<t_system_property_type, PropertyTypeBasicDto>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
				.ForMember(dest => dest.LanguageKey, opt => opt.MapFrom(src => src.language_key));

			CreateMap<t_system_property, SystemPropertyBasicDto>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
				.ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.t_system_property_type_id))
				.ForMember(dest => dest.LanguageKey, opt => opt.MapFrom(src => src.language_key));
		}
	}
}
