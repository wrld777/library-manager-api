using FluentValidation;
using MediatR;
using FluentValidation.Results;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.ApplicationCore.Common.Behaviors
{
    public class Validatebehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    { 
        public async Task<TResponse> Handle( TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(!validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);

            var results = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var faliures = results
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (faliures.Count != 0)
            {
                throw new ValidationException(faliures);
            }

            return await next();
        }

    }
}
