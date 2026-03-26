using byLiGG.Domain.Language.Resources;
using System.Globalization;

namespace byLiGG.Domain.Language.Helpers
{
	public class uiTextHelper
	{
		public static string GetUiMessage(string languageKey)
		{
			return uiText.ResourceManager.GetString(languageKey, CultureInfo.CurrentUICulture);
		}
	}
}