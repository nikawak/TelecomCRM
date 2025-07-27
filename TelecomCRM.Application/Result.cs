using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelecomCRM.Application
{
    public class Result<TValue>
    {
        public bool IsSuccess { get; }
        public TValue? Value { get; }
        public Error? ErrorName { get; }

        private Result(TValue? value, Error? error, bool isSuccess)
        {
            Value = value;
            ErrorName = error;
            IsSuccess = isSuccess;
        }

        public static Result<TValue> Success(TValue value) =>
            new(value, default, true);

        public static Result<TValue> Failure(Error error) =>
            new(default, error, false);
    }

}


