namespace VidiSnizenja.Shared.Result;

public class Result<TEntity> : Result
{
    public TEntity Data { get; set; }

    public Result(TEntity data, bool success = true, string error = "") : base(success, error)
    {
        Data = data;
    }
}
