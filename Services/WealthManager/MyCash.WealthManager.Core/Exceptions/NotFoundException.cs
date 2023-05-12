using Micro.Exceptions;

namespace MyCash.WealthManager.Core.Exceptions;

public class NotFoundException : CustomException
{
    public NotFoundException(string msg = "Podany identyfikator nie istnieje.")
        : base(msg)
    {
    }
}
