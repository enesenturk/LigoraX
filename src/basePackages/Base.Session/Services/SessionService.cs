using Base.Session.Extensions;
using Microsoft.AspNetCore.Http;

namespace Base.Session.Services
{
	public class SessionService : ISessionService
	{

		#region CTOR

		private IHttpContextAccessor _httpContextAccessor;

		public SessionService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		#endregion

		public T Get<T>(string key) where T : class
		{
			return _httpContextAccessor.HttpContext.Session == null
				? null
				: _httpContextAccessor.HttpContext.Session.GetObject<T>(key);
		}

		public void Set(string key, object value)
		{
			_httpContextAccessor.HttpContext.Session.SetObject(key, value);
		}

		public void Remove(string key)
		{
			_httpContextAccessor.HttpContext.Session.Remove(key);
		}

	}
}
