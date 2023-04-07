using MyCash.Users.Core.ValueObjects;

namespace MyCash.Users.Core.Entities;

public class User
{
    public Guid Id { get; set; }
    public string RoleId { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public string? Password { get; set; } = string.Empty;
    public UserPackage Package { get; set; } = null!;    
    public DateTime CreatedAt { get; set; }


    public Role Role { get; set; } = new();
}
