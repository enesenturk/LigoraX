using Base.Logging.Loggers;
using Base.Logging.Loggers.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Reflection;

namespace Base.Logging.Pipelines
{
	public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	where TRequest : IRequest<TResponse>
		//where TRequest : IRequest<TResponse>, ILoggableRequest
	{

		private readonly ILoggerService _loggerService;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public LoggingBehavior(ILoggerService loggerService, IHttpContextAccessor httpContextAccessor)
		{
			_loggerService = loggerService;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			string requestLog = GetLogDetail(request);

			_loggerService.LogInfo($"REQUEST:\n{requestLog}");

			TResponse response = await next();

			string responseLog = GetLogDetail(response);

			_loggerService.LogInfo($"RESPONSE:\n{responseLog}");

			return response;
		}

		private string GetLogDetail<T>(T obj)
		{
			if (obj == null)
				return "null";

			T simplifiedObj = ReplaceBase64Fields(obj);

			LogDetail logDetail = new LogDetail
			{
				MethodName = simplifiedObj.GetType().Name,
				Parameters = simplifiedObj,
				User = GetUser()
			};

			string log = JsonConvert.SerializeObject(logDetail, Formatting.Indented);

			return log;
		}

		private T ReplaceBase64Fields<T>(T obj)
		{
			T clone = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj));

			var type = typeof(T);
			var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

			foreach (var prop in props)
			{
				if (!prop.CanRead || !prop.CanWrite)
					continue;

				if (prop.PropertyType != typeof(string))
					continue;

				var propName = prop.Name.ToLower();
				string value = prop.GetValue(clone) as string;

				if (propName.Contains("base64") && !string.IsNullOrEmpty(value) && value.Length > 50)
				{
					long byteLength = (value.Length * 3L) / 4;
					long kb = byteLength / 1024;

					prop.SetValue(clone, $"***{kb}KB base64 content omitted***");
				}
			}

			return clone;
		}

		private string GetUser()
		{
			if (_httpContextAccessor.HttpContext is null ||
				_httpContextAccessor.HttpContext.User is null ||
				_httpContextAccessor.HttpContext.User.Claims is null
				)
				return "?";

			var claim = _httpContextAccessor.HttpContext.User.Claims.ToArray()[3];

			if (claim is null)
				return "?";

			string claimText = claim.ToString();
			string[] claimInfo = claimText.Split("emailaddress: ");

			if (claimInfo.Length != 2)
				return "?";

			string email = claimInfo[1];

			return email;
		}

	}
}