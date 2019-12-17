using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Utils
{
    public class Result
    {
        public bool Success { get; private set; }
        public string Error { get; private set; }

        protected Result(bool success, string error)
        {
            Success = success;
            Error = error;
        }

        public static Result Fail(string message) => new Result(false, message);

        public static Result<T> Fail<T>(string message) => new Result<T>(false, message);

        public static Result<T> Ok<T>(T value) => new Result<T>(value, true, "");

        public static Result Ok() => new Result(true, "");
    }

    public class Result<T> : Result
    {
        public T Value { get; set; }

        protected internal Result(T value, bool success, string error)
            : base(success, error)
        {
            Value = value;
        }

        protected internal Result(bool success, string error)
            : base(success, error)
        {
        }
    }

}