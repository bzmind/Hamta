using System.Text;
using Common.Application.Exceptions;
using FluentValidation;
using MediatR;

namespace Common.Application.Utility.Validation;

public class CommandValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public CommandValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        var errors = _validators
            .Select(v => v.Validate(request))
            .SelectMany(res => res.Errors)
            .Where(err => err != null)
            .ToList();

        if (errors.Any())
        {
            var stringBuilder = new StringBuilder();
            foreach (var error in errors)
            {
                stringBuilder.Append(error.ErrorMessage + "\n");
            }
            
            throw new InvalidCommandException(stringBuilder.ToString());
        }

        var response = await next();
        return response;
    }
}