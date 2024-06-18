using FluentValidation;
using MediatR;

namespace VidiSnizenja.Application.Behaviours;

public class ValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return next();
        }

        //var context = new ValidationContext<TRequest>(request);

        //var errorsDictionary = validators.Select(v => v.Validate(context))
        //                                  .SelectMany(v => v.Errors)
        //                                  .Where(v => v != null)
        //                                  .GroupBy(v => v.PropertyName, v => v.ErrorMessage, (propertyName, errorMessages) => new KeyValuePair<string, IEnumerable<string>>(propertyName, errorMessages.Distinct().ToList()))
        //                                  .ToDictionary(v => v.Key, v => v.Value);

        var ValidationErrors = validators.Select(v => v.Validate(request))
                                          .SelectMany(v => v.Errors)
                                          .Where(v => v != null)
                                          .ToList();

        if (ValidationErrors.Any())
        {
            throw new ValidationException(ValidationErrors);
        }

        return next();
    }
}
