namespace VidiSnizenja.Shared.Result;
public class Result
{
    public bool IsSuccess { get; }
    public string Error { get; private set; }
    public bool IsFailure => !IsSuccess;

    protected Result(bool isSuccess = true, string error = "")
    {
        if (!isSuccess && !string.IsNullOrEmpty(error))
        {
            throw new ArgumentException();
        }

        if (!isSuccess && string.IsNullOrEmpty(error))
        {
            throw new ArgumentException();
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Fail(string message)
    {
        return new Result(false, message);
    }

    public static Result<TEntity> Fail<TEntity>(string message)
    {
        return new Result<TEntity>(default, false, message);
    }

    public static Result Ok(string message)
    {
        return new Result(true, message);
    }

    public static Result<TEntity> Ok<TEntity>(TEntity data, string message = null)
    {
        return new Result<TEntity>(data, true, message);
    }
}
