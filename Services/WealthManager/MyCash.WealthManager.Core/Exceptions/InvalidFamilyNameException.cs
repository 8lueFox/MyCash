using Micro.Exceptions;

namespace MyCash.WealthManager.Core.Exceptions;

public class InvalidFamilyNameException : CustomException
{
    public string Name { get; set; }
    public InvalidFamilyNameException(string name)
        : base($"Invalid family name: '{name}'.")
    {
        Name = name;
	}
}
