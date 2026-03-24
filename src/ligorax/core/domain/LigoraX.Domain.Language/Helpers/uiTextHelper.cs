using LigoraX.Domain.Language.Resources;
using System.Globalization;

namespace LigoraX.Domain.Language.Helpers
{
	public class uiTextHelper
	{
		public static string GetUiMessage(string languageKey)
		{
			return uiText.ResourceManager.GetString(languageKey, CultureInfo.CurrentUICulture);
		}
	}
}