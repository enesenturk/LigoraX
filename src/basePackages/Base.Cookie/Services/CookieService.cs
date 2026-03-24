using Base.Cookie.Extensions;
using Base.Cookie.Models;
using Base.PrimitiveTypeHelpers._DateTime.Entensions;
using Microsoft.AspNetCore.Http;

namespace Base.Cookie.Services
{
	public class CookieService : ICookieService
	{

		#region CTOR

		private readonly TimeSpan _defaultExpiration;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CookieService(IHttpContextAccessor httpContextAccessor, TimeSpan defaultExpiration)
		{
			_httpContextAccessor = httpContextAccessor;
			_defaultExpiration = defaultExpiration;
		}

		#endregion

		public string Get(string key)
		{
			string cookie = _httpContextAccessor.HttpContext.Request.Cookies[key];

			bool hasCookie = !string.IsNullOrEmpty(cookie);

			return hasCookie ? cookie : null;
		}

		public List<string> GetAllKeys()
		{
			return _httpContextAccessor.HttpContext.Request.Cookies.Select(x => x.Key).ToList();
		}

		public void Set(CookieModel model)
		{
			if (model.Expiration is null)
				model.Expiration = DateTime.Now.ToUniversalTimeZone().Add(_defaultExpiration);

			CookieOptions cookieOptions = new CookieOptions
			{
				Expires = model.Expiration,
				HttpOnly = model.IsHttpOnly
			};

			if (model.IsJson)
				_httpContextAccessor.HttpContext.Response.Cookies.Append(model.Key, model.Value, cookieOptions);
			else
				_httpContextAccessor.HttpContext.Response.AppendUnencodedCookie(model.Key, model.Value, cookieOptions);
		}

		public void Remove(string key)
		{
			_httpContextAccessor.HttpContext.Response.Cookies.Delete(key);
		}

	}
}
