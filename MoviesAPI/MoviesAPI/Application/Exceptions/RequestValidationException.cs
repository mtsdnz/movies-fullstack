namespace MoviesAPI.Application.Exceptions;

public class RequestValidationException(Dictionary<string, string[]> errors) : Exception
{
    public Dictionary<string, string[]> Errors { get; } = errors;
}