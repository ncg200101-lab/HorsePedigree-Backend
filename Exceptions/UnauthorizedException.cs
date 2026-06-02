namespace HorsePedigree_2026.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException(string message = "No autorizado.") : base(message)
    {
    }
}
