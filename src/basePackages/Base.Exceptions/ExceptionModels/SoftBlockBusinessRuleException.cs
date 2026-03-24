namespace Base.Exceptions.ExceptionModels
{
	public class SoftBlockBusinessRuleException : Exception
	{

		public SoftBlockBusinessRuleException()
		{

		}

		public SoftBlockBusinessRuleException(string message, string code = "")
			: base(message, new Exception($"{code}"))
		{

		}

	}
}
