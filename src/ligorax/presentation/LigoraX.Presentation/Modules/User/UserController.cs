using Base.Dto.BaseResponse;
using LigoraX.Application.UseCases.Modules.User.Commands.CreateUserRegistrationCommand.Dtos;
using LigoraX.Application.UseCases.Modules.User.Queries.GetUserLoginQuery.Dtos;
using LigoraX.Presentation.Base.Controllers;
using LigoraX.Presentation.Modules.User.Models;
using Microsoft.AspNetCore.Mvc;

namespace LigoraX.Presentation.Modules.User
{
	[Route("api/user")]
	public class UserController : BaseController
	{

		#region CTOR

		#endregion

		[HttpPost("register")]
		public async Task<ObjectResult> Register([FromBody] Create_UserRegistration_RequestModel requestModel)
		{
			Create_UserRegistration_CommandDto command = _mapper.Map<Create_UserRegistration_CommandDto>(requestModel);

			Create_UserRegistration_ResponseDto response = await _mediator.Send(command);

			return Ok(new BaseResponseDto
			{
				ResponseModel = response
			});
		}

		[HttpPost("login")]
		public async Task<ObjectResult> Login([FromBody] Get_UserLogin_RequestModel requestModel)
		{
			Get_UserLogin_QueryDto query = _mapper.Map<Get_UserLogin_QueryDto>(requestModel);

			Get_UserLogin_ResponseDto response = await _mediator.Send(query);

			return Ok(new BaseResponseDto
			{
				ResponseModel = response
			});
		}

	}
}