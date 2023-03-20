namespace MyCash.Users.Core.Exceptions;

internal class InvalidCredentialsException : Exception
{
    public InvalidCredentialsException()
        : base("Invalid credentials.")
    {
    }
}
