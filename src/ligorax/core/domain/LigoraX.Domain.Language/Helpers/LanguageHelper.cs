using Base.Exceptions.ExceptionModels;

namespace LigoraX.Domain.Language.Helpers
{
	public class LanguageHelper
	{

		public static List<string> GetSupportedLanguages()
		{
			return new List<string>
			{
				"en-US",
				"tr-TR",
			};
		}

		public static string GetDefaultLanguage()
		{
			return GetSupportedLanguages().First();
		}

		public static string GetSupportedLanguage(string language)
		{
			List<string> supportedLanguages = GetSupportedLanguages();

			if (supportedLanguages.Contains(language))
				return language;

			throw new AuthorizationException();
		}

		public static bool IsSupportedLanguage(string language)
		{
			List<string> supportedLanguages = GetSupportedLanguages();

			return supportedLanguages.Contains(language);
		}

	}
}
