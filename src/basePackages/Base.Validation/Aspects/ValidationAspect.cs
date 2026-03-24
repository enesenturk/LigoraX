using AspectCore.DynamicProxy;
using FluentValidation;

namespace Base.Validation.Aspects
{
	[AttributeUsage(AttributeTargets.Method)]
	public class ValidationAspectAttribute : AbstractInterceptorAttribute
	{
		public Type ValidatorType { get; set; }

		public override async Task Invoke(AspectContext context, AspectDelegate next)
		{
			IValidator validator = (IValidator)Activator.CreateInstance(ValidatorType);

			Type typeToValidate = ValidatorType.BaseType.GetGenericArguments()[0];

			IEnumerable<object> argsToValidate = context.Parameters.Where(t => t.GetType() == typeToValidate);

			foreach (object argToValidate in argsToValidate)
			{
				FluentValidate(validator, argToValidate);
			}

			await next(context);
		}

		private void FluentValidate(IValidator validator, object arg)
		{
			var context = new ValidationContext<object>(arg);
			var result = validator.Validate(context);

			if (result.Errors.Count > 0)
				throw new ValidationException(result.Errors);
		}

	}
}
