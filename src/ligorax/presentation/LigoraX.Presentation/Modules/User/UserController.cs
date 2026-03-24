using LigoraX.Presentation.Base.Controllers;

namespace LigoraX.Presentation.Modules.User
{
	public class UserController : BaseController
	{

		#region CTOR

		private readonly UserControllerInternal _controllerInternal;

		public UserController(UserControllerInternal controllerInternal)
		{
			_controllerInternal = controllerInternal;
		}

		#endregion

	}
}