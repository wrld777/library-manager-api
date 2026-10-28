using FluentValidation;
using MediatR;
using FluentValidation.Results;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using LibraryManager.ApplicationCore.ResultPattern;

namespace LibraryManager.ApplicationCore.Common.Behaviors
{
    public class Validatebehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    where TResponse : Result
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
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
                var error = new Error("Validation.Failed", string.Join(";", faliures.Select(f => f.ErrorMessage)), ErrorType.Validation);
                return CreateFaliure(error);
            }

            return await next();
        }

        public static TResponse CreateFaliure(Error error)
        {
            //Result generico
            if(typeof(TResponse) == typeof(Result))
                return (TResponse)Result.Failure(error);

            //Result con Result<T>

            var failure = typeof(TResponse).GetMethod(
                nameof(Result.Failure),
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static,
                new[] { typeof(Error) });

            return (TResponse)failure!.Invoke(null, new object[] { error })!;
        }

    }
}
