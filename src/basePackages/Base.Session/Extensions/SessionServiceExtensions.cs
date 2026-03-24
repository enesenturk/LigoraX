using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Base.Session.Extensions
{
	internal static class SessionServiceExtensions
	{

		internal static void SetObject(this ISession session, string key, object data)
		{
			string objectString = JsonConvert.SerializeObject(data);
			session.SetString(key, objectString);
		}

		internal static T GetObject<T>(this ISession session, string key) where T : class
		{
			string objectString = session.GetString(key);

			if (string.IsNullOrEmpty(objectString))
				return null;

			return JsonConvert.DeserializeObject<T>(objectString);
		}

	}
}
