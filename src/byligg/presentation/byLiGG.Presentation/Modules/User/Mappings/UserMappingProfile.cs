using AutoMapper;
using byLiGG.Application.UseCases.Modules.User.Commands.CreateUserRegistrationCommand.Dtos;
using byLiGG.Application.UseCases.Modules.User.Queries.GetUserLoginQuery.Dtos;
using byLiGG.Presentation.Modules.User.Models;

namespace byLiGG.Presentation.Modules.User.Mappings
{
	public class UserMappingProfile : Profile
	{
		public UserMappingProfile()
		{
			CreateMap<Create_UserRegistration_RequestModel, Create_UserRegistration_CommandDto>();
			CreateMap<Get_UserLogin_RequestModel, Get_UserLogin_QueryDto>();
		}
	}
}