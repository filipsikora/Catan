using Catan.Shared.Data;

namespace Catan.Core.Results
{
    public sealed class Result<T>
    {
        public bool Success { get; }
        public T? Value { get; }
        public ConditionFailureReason Reason { get; }

        private Result(bool success, T? value, ConditionFailureReason reason)
        {
            Success = success;
            Value = value;
            Reason = reason;
        }

        public static Result<T> Ok(T value)
            => new(true, value, ConditionFailureReason.None);

        public static Result<T> Fail(ConditionFailureReason reason)
            => new(false, default, reason);
    }
}