namespace MyCash.Users.Core.Exceptions;

internal class MissingPasswordException : Exception
{
    public MissingPasswordException()
        : base("Invalid password.")
    {
    }
}
