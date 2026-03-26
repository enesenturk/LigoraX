namespace byLiGG.Configuration.AppSettings
{
	public class JwtSettings
	{

		public static string SecretKey { get; set; }
		public static string Issuer { get; set; }
		public static string Audience { get; set; }
		public static int ExpiryDays { get; set; }

	}
}
