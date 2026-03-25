using AutoMapper;
using LigoraX.Application.UseCases.Modules.User.Commands.CreateUserRegistrationCommand.Dtos;
using LigoraX.Application.UseCases.Modules.User.Queries.GetUserLoginQuery.Dtos;
using LigoraX.Presentation.Modules.User.Models;

namespace LigoraX.Presentation.Modules.User.Mappings
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