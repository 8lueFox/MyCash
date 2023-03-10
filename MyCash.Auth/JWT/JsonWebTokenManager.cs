using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyCash.Micro.Time;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MyCash.Auth.JWT;

internal sealed class JsonWebTokenManager : IJsonWebTokenManager
{
    private static readonly Dictionary<string, IEnumerable<string>> EmptyClaims = new();
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();
    private readonly string? _issuer;
    private readonly TimeSpan _expiry;
    private readonly IClock _clock;
    private readonly SigningCredentials _singingCredentials;
    private readonly string? _audience;

    public JsonWebTokenManager(IOptions<AuthOptions> options, SecurityKeyDetails securityKeyDetails, IClock clock)
    {
        if (options.Value?.Jwt is null)
            throw new InvalidOperationException("Missing JWT options.");

        _audience = options.Value.Jwt.Audience;
        _issuer = options.Value.Jwt.Issuer;
        _expiry = options.Value.Jwt.Expiry ?? TimeSpan.FromHours(1);
        _clock = clock;
        _singingCredentials = new SigningCredentials(securityKeyDetails.Key, securityKeyDetails.Algorithm);
    }

    public JsonWebToken CreateToken(string userId, string? email = null, string? role = null,
        IDictionary<string, IEnumerable<string>>? claims = null)
    {
        var now = _clock.Current();
        var jwtClaims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId),
            new(JwtRegisteredClaimNames.UniqueName, userId)
        };

        if (!email.IsNullOrEmpty())
            jwtClaims.Add(new(JwtRegisteredClaimNames.Email, email!));

        if (!role.IsNullOrEmpty())
            jwtClaims.Add(new(ClaimTypes.Role, role!));

        if (!_audience.IsNullOrEmpty())
            jwtClaims.Add(new(JwtRegisteredClaimNames.Aud, _audience!));

        if(claims?.Any() is true)
        {
            var customerClaims = new List<Claim>();
            foreach (var (claim, values) in claims)
            {
                customerClaims.AddRange(values.Select(value => new Claim(claim, value)));
            }

            jwtClaims.AddRange(customerClaims);
        }

        var expires = now.Add(_expiry);

        var jwt = new JwtSecurityToken(_issuer, claims: jwtClaims, notBefore: now, expires: expires, signingCredentials: _singingCredentials);

        var token = _jwtSecurityTokenHandler.WriteToken(jwt);

        return new JsonWebToken
        {
            AccessToken = token,
            Expiry = new DateTimeOffset(expires).ToUnixTimeMilliseconds(),
            UserId = userId,
            Email = email ?? string.Empty,
            Role = role ?? string.Empty,
            Claims = claims ?? EmptyClaims
        };
    }
}
