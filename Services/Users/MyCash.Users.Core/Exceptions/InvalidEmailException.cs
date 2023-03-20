namespace MyCash.Users.Core.Exceptions;

internal class InvalidEmailException : Exception
{
    public string Email { get; }

    public InvalidEmailException(string email)
        : base($"Email is invalid: '{email}'.")
    {
        Email = email;
    }
}
