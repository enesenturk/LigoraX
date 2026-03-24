using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace Base.Cookie.Extensions
{
	internal static class CookieServiceExtensions
	{

		internal static void AppendUnencodedCookie(this HttpResponse response, string key, string value, CookieOptions options)
		{
			if (options == null)
				throw new ArgumentNullException(nameof(options));

			response.Cookies.Delete(key);

			value = value.Replace(" ", "+");

			var setCookieHeaderValue = new SetCookieHeaderValue(key, value)
			{
				Domain = options.Domain,
				Path = options.Path,
				Expires = options.Expires,
				MaxAge = options.MaxAge,
				Secure = options.Secure,
				SameSite = (Microsoft.Net.Http.Headers.SameSiteMode)options.SameSite,
				HttpOnly = options.HttpOnly
			};

			response.Headers[HeaderNames.SetCookie] = StringValues.Concat(response.Headers[HeaderNames.SetCookie], setCookieHeaderValue.ToString());
		}
	}
}
