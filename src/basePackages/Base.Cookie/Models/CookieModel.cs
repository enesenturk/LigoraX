namespace Base.Cookie.Models
{
	public class CookieModel
	{

		public string Key { get; set; }
		public string Value { get; set; }
		public DateTime? Expiration { get; set; }
		public bool IsHttpOnly { get; set; } = true;
		public bool IsJson { get; set; }

	}
}
