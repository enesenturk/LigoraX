using byLiGG.Configuration.AppSettings;
using byLiGG.Domain.Language.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace byLiGG.Presentation.Filters
{
	public class TrackExecution : ActionFilterAttribute
	{

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (!IsCorrectApiKey(filterContext) || !IsCorrectLanguage(filterContext))
			{
				filterContext.Result = new UnauthorizedResult();

				return;
			}

			string actionName = filterContext.RouteData.Values["action"].ToString();
			string controllerName = filterContext.RouteData.Values["controller"].ToString();

			if (IsAuthenticationFree(controllerName, actionName))
				return;

			if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
			{
				filterContext.Result = new UnauthorizedResult();

				return;
			}
		}

		#region Behind the Scenes

		private bool IsCorrectApiKey(ActionExecutingContext filterContext)
		{
			string receivedKey = filterContext.HttpContext.Request.Headers["X-Api-Key"].FirstOrDefault();

			return !string.IsNullOrEmpty(receivedKey) && receivedKey == ProjectSettings.ClientApiKey;
		}

		private bool IsCorrectLanguage(ActionExecutingContext filterContext)
		{
			string language = filterContext.HttpContext.Request.Headers["Accept-Language"].FirstOrDefault();

			return !string.IsNullOrEmpty(language) && LanguageHelper.IsSupportedLanguage(language);
		}

		private bool IsAuthenticationFree(string controllerName, string actionName)
		{
			return
				controllerName == "User" && actionName == "Register" ||
				controllerName == "User" && actionName == "Login" ||
				controllerName == "User" && actionName == "Logout";
		}

		#endregion

	}
}