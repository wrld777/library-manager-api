using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.ApplicationCore.ResultPattern
{
    //categoria di errori
    public enum ErrorType
    {
        OldPasswordMatchingError = 400,
        PasswordMatchingError = 400,
        AlreadyVerificated = 409,
        Validation = 400,
        InvalidFormat = 422,
        InvalidOperation = 409,
        MissingRequiredField = 400,
        OutOfRange = 400,
        Unauthorized = 401,
        Forbidden = 403,
        TokenExpired = 401,
        InvalidCredentials = 401,
        NotFound = 404,
        AlreadyExists = 409,
        Conflict = 409,
        Timeout = 408,
        ServiceUnavailable = 503,
        NetworkError = 502,
        InternalServerError = 500,
        DatabaseError = 500,
        ConfigurationError = 500,
        Unknown = 520,
        OperationFailed = 424
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
        public static Error BookAlreadyExists { get; } = new("BookAlreadyExists", "The book already exists.", ErrorType.AlreadyExists);

    }
}
