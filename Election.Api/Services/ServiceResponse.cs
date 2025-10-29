namespace Election.Api.Services;

public sealed class ServiceResponse
{
    public string? ErrorMessage { get; init; }
    public bool IsError => !string.IsNullOrEmpty(ErrorMessage);

    public static ServiceResponse Ok() => new();
    public static ServiceResponse Error(string errorMessage) => new() { ErrorMessage = errorMessage };
}

public sealed class ServiceResponse<T>
{
    public T? Value { get; init; } = default(T);
    public string? ErrorMessage { get; init; }
    public bool IsError => !string.IsNullOrEmpty(ErrorMessage);

    public static ServiceResponse<T> Ok(T value) => new() { Value = value };
    public static ServiceResponse<T> Error(string errorMessage) => new() { ErrorMessage = errorMessage };
}