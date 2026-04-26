namespace EnhancedCVAgent.Domain.Exceptions;

public abstract class DomainException : Exception
{
    protected DomainException(){}
    protected DomainException(string message) : base(message){}
    protected DomainException(string message, Exception innerException) : base(message, innerException) {}
}

public sealed class DomainValidationException : DomainException
{
    public DomainValidationException(string message) : base(message) {}
    public DomainValidationException(string message, Exception innerException) : base(message, innerException) {}
}