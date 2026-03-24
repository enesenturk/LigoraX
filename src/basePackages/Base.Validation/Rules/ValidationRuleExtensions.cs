using Base.PrimitiveTypeHelpers._DateTime.Entensions;
using FluentValidation;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Base.Validation.Rules
{
	public static class ValidationRuleExtensions
	{

		public static IRuleBuilderOptions<T, DateTime> NoFutureDate<T>(
			this IRuleBuilder<T, DateTime> ruleBuilder)
		{
			DateTime utcNow = DateTime.Now.ToUniversalTimeZone();

			return ruleBuilder.Must(value => value < utcNow);
		}

		public static IRuleBuilderOptions<T, string> NoInvalidEmail<T>(
			this IRuleBuilder<T, string> ruleBuilder)
		{
			return ruleBuilder.Must(value =>
			{
				try
				{
					MailAddress mail = new MailAddress(value);
					return true;
				}
				catch
				{
					return false;
				}
			});
		}

		public static IRuleBuilderOptions<T, string> NoInvalidPhoneNumber<T>(
			this IRuleBuilder<T, string> ruleBuilder)
		{
			return ruleBuilder.Must(value =>
			{
				if (value == null)
					return false;

				string pattern = @"(\(0\d\d\)\s\d{3}[\s-]+\d{4})|(0\d\d[\s-]+\d{3}[\s-]+\d{4})|(0\d{9})|(\+\d\d\s?[\(\s]\d\d[\)\s]\s?\d{3}[\s-]?\d{4})";
				Match match = Regex.Match(value, pattern, RegexOptions.IgnoreCase);

				return match.Success;
			});
		}

		public static IRuleBuilderOptions<T, string> NoSpecialChars<T>(
			this IRuleBuilder<T, string> ruleBuilder)
		{
			return ruleBuilder.Must(value =>
			{
				if (value is null)
					return false;

				string pattern = @"^[a-zA-Z0-9 ]*$";
				return Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase);
			});
		}

		public static IRuleBuilderOptions<T, string> NoWhiteSpace<T>(
			this IRuleBuilder<T, string> ruleBuilder)
		{
			return ruleBuilder.Must(value => value == null || !value.Any(char.IsWhiteSpace));
		}

		public static IRuleBuilderOptions<T, string> OnlyNumber<T>(
			this IRuleBuilder<T, string> ruleBuilder)
		{
			return ruleBuilder.Matches(@"^\d+$");
		}
	}
}
