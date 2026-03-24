using Base.Exceptions.ExceptionModels;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Base.Validation.Pipelines
{
	public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>, IValidationRequest
	{
		private readonly IEnumerable<IValidator<TRequest>> _validators;

		public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
		{
			_validators = validators;
		}

		public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			if (_validators.Count() is 0)
				throw new AbsurdOperationException("IValidationRequest requires a validator!");

			ValidationContext<object> context = new(request);

			List<ValidationFailure> failures = _validators
											   .Select(validator => validator.Validate(context))
											   .SelectMany(result => result.Errors)
											   .Where(failure => failure != null)
											   .ToList();

			if (failures.Count != 0)
				throw new ValidationException(failures);

			return next();
		}
	}
}