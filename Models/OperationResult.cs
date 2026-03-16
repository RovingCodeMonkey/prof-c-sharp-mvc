namespace Web.Models
{
    public class OperationResult<T>
    {
        public T? Value { get; private init; }
        public string? Error { get; private init; }
        public bool IsSuccess => Error is null;

        public static OperationResult<T> Success(T value) => new() { Value = value };
        public static OperationResult<T> Failure(string error) => new() { Error = error };
    }
}
