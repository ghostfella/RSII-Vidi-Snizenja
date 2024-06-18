namespace VidiSnizenja.Application.Exceptions;

internal class NotFoundException : Exception
{
    public Type Type { get; }

    public NotFoundException(string message, Type type) : base(message)
    {
        Type = type;
    }
}
