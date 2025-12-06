namespace PersonalAssistantApi.Application.Common;

public class Result<T>
{
    public bool Succeeded { get; }
    public T? Data { get; }
    public string Error { get; }

    private Result(bool succeeded, T? data, string error)
    {
        Succeeded = succeeded;
        Data = data;
        Error = error;
    }

    public static Result<T> Success(T data) => new(true, data, string.Empty);
    public static Result<T> Failure(string error) => new(false, default, error);
}
