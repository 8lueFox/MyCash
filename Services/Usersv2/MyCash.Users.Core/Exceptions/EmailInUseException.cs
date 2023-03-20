namespace MyCash.Users.Core.Exceptions;

internal class EmailInUseException : Exception
{
    public string Email { get; }

    public EmailInUseException(string email)
        :base($"Email '{email}' is already in use.")
    {
        Email = email;
    }
}
