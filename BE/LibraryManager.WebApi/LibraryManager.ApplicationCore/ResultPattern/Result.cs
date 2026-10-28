using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.ApplicationCore.ResultPattern
{
    //categoria di errori
    public enum ErrorType
    {
        Validation,
        NotFound,
        Conflict,
        Unauthorized,
        Forbidden,
        Failure
    }

    // creo oggetto error con codice e messaggio
    public sealed record Error(string code, string message, ErrorType Type )
    {
        private readonly static Error none = new (string.Empty, string.Empty, ErrorType.Validation);
    }

    public class Result
    {
        public bool IsSuccess { get; }
        public Error? Error { get; }

        protected Result(bool isSuccess, Error? error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new(true, null);
        public static Result Failure(Error error) => new(false, error);
    }


    public class Result<T> : Result
    {
        public T? Value { get; }
        private Result(T value) : base(true, null)
        {
            Value = value;
        }

        private Result(Error error) : base(false, error)
        {
            Value = default;

        }

        public static Result<T> Success(T value) => new(value);
        public static Result<T> Failure(Error error) => new(error);

    }


    public static class Errors
    {
        public static Error BookAlredyDeleted { get; } = new("BookAlredyDeleted", "The book has already been deleted.", ErrorType.Forbidden);

        ///
        public static Error BookNotFound { get; } = new("BookNotFound", "The book was not found.", ErrorType.NotFound);

        ///
        public static Error BookAlreadyExists { get; } = new("BookAlreadyExists", "The book already exists.", ErrorType.Forbidden);

    }
}
