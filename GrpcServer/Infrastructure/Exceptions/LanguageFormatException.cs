namespace GrpcServer.Infrastructure.Exceptions;

public sealed class LanguageFormatException : Exception
{
    public LanguageFormatException() : base("Language should contain only russian letters")
    { }
}