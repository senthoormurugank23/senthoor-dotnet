using System;
using System.Collections.Generic;
using System.Linq;

namespace BaseDotnet.Domain.Common
{
    public class Result
    {
        public bool IsSuccess { get; protected set; }
        public string Message { get; protected set; } = string.Empty;
        public List<string> Errors { get; protected set; } = new List<string>();
        public Dictionary<string, List<string>> ValidationErrorDetails { get; protected set; } = new Dictionary<string, List<string>>();

        public string HtmlFormattedError
        {
            get
            {
                var allErrors = new List<string>();
                if (!string.IsNullOrEmpty(Message))
                {
                    allErrors.Add(Message);
                }
                allErrors.AddRange(Errors);
                foreach (var detail in ValidationErrorDetails)
                {
                    allErrors.Add($"{detail.Key}: {string.Join(", ", detail.Value)}");
                }
                return string.Join("<br />", allErrors);
            }
        }

        protected Result() { }

        protected Result(bool isSuccess, string message, List<string>? errors = null, Dictionary<string, List<string>>? validationErrors = null)
        {
            IsSuccess = isSuccess;
            Message = message;
            Errors = errors ?? new List<string>();
            ValidationErrorDetails = validationErrors ?? new Dictionary<string, List<string>>();
        }

        public static Result Success(string message = "")
        {
            return new SuccessResult(message);
        }

        public static Result<T> Success<T>(T value, string message = "")
        {
            return new SuccessResult<T>(value, message);
        }

        public static Result Failure(string message, List<string>? errors = null)
        {
            return new FailureResult(message, errors);
        }

        public static Result Failure(Dictionary<string, List<string>> validationErrors, string message = "Validation failed")
        {
            return new FailureResult(message, validationErrors);
        }

        public static Result<T> Failure<T>(string message, List<string>? errors = null)
        {
            return new FailureResult<T>(message, errors);
        }

        public static Result<T> Failure<T>(Dictionary<string, List<string>> validationErrors, string message = "Validation failed")
        {
            return new FailureResult<T>(message, validationErrors);
        }
    }

    public class Result<T> : Result
    {
        public T? Value { get; protected set; }

        protected Result() { }

        internal Result(bool isSuccess, T? value, string message, List<string>? errors = null, Dictionary<string, List<string>>? validationErrors = null)
            : base(isSuccess, message, errors, validationErrors)
        {
            Value = value;
        }

        public static implicit operator Result<T>(T value) => Success(value);
    }

    public class SuccessResult : Result
    {
        public SuccessResult(string message = "") 
            : base(true, message, null, null)
        {
        }
    }

    public class SuccessResult<T> : Result<T>
    {
        public SuccessResult(T value, string message = "") 
            : base(true, value, message, null, null)
        {
        }
    }

    public class FailureResult : Result
    {
        public FailureResult(string message, List<string>? errors = null) 
            : base(false, message, errors, null)
        {
        }

        public FailureResult(string message, Dictionary<string, List<string>> validationErrors) 
            : base(false, message, null, validationErrors)
        {
        }
    }

    public class FailureResult<T> : Result<T>
    {
        public FailureResult(string message, List<string>? errors = null) 
            : base(false, default, message, errors, null)
        {
        }

        public FailureResult(string message, Dictionary<string, List<string>> validationErrors) 
            : base(false, default, message, null, validationErrors)
        {
        }
    }
}
