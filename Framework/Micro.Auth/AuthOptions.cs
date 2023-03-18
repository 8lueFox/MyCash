namespace Micro.Auth;

public sealed class AuthOptions
{
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public TimeSpan? Expiry { get; set; }
    public string? IssuerSigningKey { get; set; }
}
