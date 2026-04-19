using FluentValidation;
using MediatR;

namespace DvdLibrary.Application.Common.Behaviors;

/// <summary>
/// Stoppar ogiltiga requests tidigt så att handlers kan fokusera på affärslogik.
/// </summary>
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Om requesten saknar validatorer släpper vi igenom den direkt.
        if (!_validators.Any())
        {
            return await next();
        }

        // Alla validatorer körs innan handlern får göra något.
        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(
            _validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .SelectMany(result => result.Errors)
            .Where(error => error is not null)
            .ToList();

        if (failures.Count != 0)
        {
            // Samlar alla fel i ett enda undantag som fångas i API-lagret.
            throw new ValidationException(failures);
        }

        return await next();
    }
}
