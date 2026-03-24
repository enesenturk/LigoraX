namespace Base.Session.Services
{
	public interface ISessionService
	{

		T Get<T>(string key) where T : class;
		void Set(string key, object value);
		void Remove(string key);

	}
}
