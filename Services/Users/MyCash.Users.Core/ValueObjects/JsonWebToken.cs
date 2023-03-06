namespace MyCash.Users.Core.ValueObjects;

public sealed class JsonWebToken
{
    public string AccessToken { get; set; } = string.Empty;
    public long Expiry { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;

    public IDictionary<string, IEnumerable<string>>? Claims { get; set; }
}
