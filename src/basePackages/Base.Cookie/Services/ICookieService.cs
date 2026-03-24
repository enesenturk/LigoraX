using Base.Cookie.Models;

namespace Base.Cookie.Services
{
	public interface ICookieService
	{
		string Get(string key);
		List<string> GetAllKeys();
		void Set(CookieModel model);
		void Remove(string key);
	}
}
