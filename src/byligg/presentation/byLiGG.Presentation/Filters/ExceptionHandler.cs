using Base.Dto.BaseResponse;
using Base.Exceptions.ExceptionModels;
using Base.Logging.Loggers;
using FluentValidation;
using byLiGG.Configuration.AppSettings;
using byLiGG.Domain.Language.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace byLiGG.Presentation.Filters
{
	public class ExceptionHandler : ExceptionFilterAttribute
	{

		public override void OnException(ExceptionContext filterContext)
		{
			LogError(filterContext);

			Type exceptionType = GetExceptionType(filterContext);

			if (exceptionType == typeof(AuthorizationException))
			{
				filterContext.ExceptionHandled = true;
				filterContext.Result = new UnauthorizedResult();
				return;
			}

			Type returnType = (filterContext.ActionDescriptor as ControllerActionDescriptor).MethodInfo.ReturnType;

			if (
				returnType == typeof(OkObjectResult) ||
				returnType == typeof(Task<OkObjectResult>) ||
				returnType == typeof(ObjectResult) ||
				returnType == typeof(Task<ObjectResult>)
				)
			{
				HandleJsonExeption(filterContext);
			}
			else
			{
				throw new AbsurdOperationException("Result type not implemented!");
			}
		}

		#region Behind the Scenes

		private Type GetExceptionType(ExceptionContext filterContext)
		{
			Exception ex = filterContext.Exception;

			if (ex is AggregateException agg && agg.InnerExceptions.Count == 1)
				return agg.InnerExceptions[0].GetType();

			return ex.GetType();
		}

		private string GetExceptionMessage(Exception ex)
		{
			if (ex is AggregateException agg && agg.InnerExceptions.Count == 1)
			{
				return GetExceptionMessage(agg.InnerExceptions[0]);
			}

			return ex.Message;
		}

		private void LogError(ExceptionContext filterContext)
		{
			ILoggerService loggerService = (ILoggerService)filterContext.HttpContext.RequestServices.GetService(typeof(ILoggerService));

			loggerService.LogError(filterContext.Exception);
		}

		private void HandleJsonExeption(ExceptionContext filterContext)
		{
			BaseResponseDto response = GetExceptionResponse(filterContext);

			filterContext.ExceptionHandled = true;
			filterContext.Result = new JsonResult(response);
		}

		private BaseResponseDto GetExceptionResponse(ExceptionContext filterContext)
		{
			string exceptionMessage = "";
			string responseType = BaseResponseConstants.TypeError;

			Type exceptionType = GetExceptionType(filterContext);

			if (exceptionType == typeof(BusinessRuleException))
			{
				exceptionMessage = GetExceptionMessage(filterContext.Exception);
			}
			else if (exceptionType == typeof(SoftBlockBusinessRuleException))
			{
				exceptionMessage = GetExceptionMessage(filterContext.Exception);
				responseType = "SB";
			}
			else if (exceptionType == typeof(NotFoundException))
			{
				exceptionMessage = uiText.Not_Found;
			}
			else if (exceptionType == typeof(ValidationException))
			{
				ValidationException validationException = (ValidationException)filterContext.Exception;
				exceptionMessage = string.Join("\n", validationException.Errors.Select(e => e.ErrorMessage));
			}
			else if (ProjectSettings.ShowException)
			{
				Exception innerException = filterContext.Exception.InnerException;
				string innerExceptionMessage = innerException != null ? innerException.Message : "";
				string inner = innerExceptionMessage != "" ? $" InnerException: {innerExceptionMessage}" : "";

				exceptionMessage = $"Message: {filterContext.Exception.Message}{inner}";
			}
			else
			{
				Exception exception = filterContext.Exception;

				while (exception.InnerException != null)
				{
					if (exception.Message.Contains("Timeout"))
					{
						exceptionMessage = uiText.Error_Occurred_Contact_Admin;
						break;
					}

					exception = exception.InnerException;
				}

				if (string.IsNullOrEmpty(exceptionMessage))
					exceptionMessage = uiText.Error_Occurred_Contact_Admin;
			}

			return new BaseResponseDto
			{
				Message = exceptionMessage.Replace("\n", "</br>"),
				Type = responseType
			};
		}

		#endregion

	}
}