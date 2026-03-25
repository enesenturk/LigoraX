using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace LigoraX.Presentation.Base.Controllers
{
	[ApiController]
	public class BaseController : ControllerBase
	{

		protected string _controllerName => ControllerContext.ActionDescriptor.ControllerName;

		private IMediator mediator;
		protected IMediator _mediator => mediator ??= HttpContext.RequestServices.GetService<IMediator>();

		private IMapper mapper;
		protected IMapper _mapper => mapper ??= HttpContext.RequestServices.GetService<IMapper>();

	}
}