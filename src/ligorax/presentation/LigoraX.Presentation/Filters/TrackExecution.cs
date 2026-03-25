using LigoraX.Domain.Language.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LigoraX.Presentation.Filters
{
	public class TrackExecution : ActionFilterAttribute
	{

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			string actionName = filterContext.RouteData.Values["action"].ToString();
			string controllerName = filterContext.RouteData.Values["controller"].ToString();

			bool skipTracking = GetDoNotTrack(controllerName, actionName);

			if (skipTracking)
				return;

			if (!IsCorrectLanguage(filterContext))
			{
				filterContext.Result = new UnauthorizedResult();

				return;
			}

			if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
			{
				filterContext.Result = new UnauthorizedResult();

				return;
			}
		}

		#region Behind the Scenes

		private bool IsCorrectLanguage(ActionExecutingContext filterContext)
		{
			string language = filterContext.HttpContext.Request.Headers["Accept-Language"].FirstOrDefault();

			return !string.IsNullOrEmpty(language) && LanguageHelper.IsSupportedLanguage(language);
		}

		private bool GetDoNotTrack(string controllerName, string actionName)
		{
			return
				controllerName == "User" && actionName == "Register" ||
				controllerName == "User" && actionName == "Login" ||
				controllerName == "User" && actionName == "Logout";
		}

		#endregion

	}
}