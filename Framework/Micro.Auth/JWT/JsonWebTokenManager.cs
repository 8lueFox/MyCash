using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Micro.Auth.JWT;

internal sealed class JsonWebTokenManager : IJsonWebTokenManager
{
    private static readonly Dictionary<string, IEnumerable<string>> EmptyClaims = new();
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();
    private readonly string? _issuer;
    private readonly string? _audience;
    private readonly TimeSpan _expiry;
    private readonly SigningCredentials _signingCredentials;

    public JsonWebTokenManager(IOptions<AuthOptions> options)
    {
        if (options.Value is null)
            throw new InvalidOperationException("Missing JWT options.");

        _issuer = options.Value.Issuer;
        _audience = options.Value.Audience;
        _expiry = options.Value.Expiry ?? TimeSpan.FromHours(1);
        var rawKey = Encoding.UTF8.GetBytes(options.Value.IssuerSigningKey);
        _signingCredentials = new SigningCredentials(new SymmetricSecurityKey(rawKey), SecurityAlgorithms.HmacSha256);
    }

    public JsonWebToken CreateToken(string userId, string? email = null, string? role = null, IDictionary<string, IEnumerable<string>>? claims = null)
    {
        ///TODO: Change getting current date from the service IClock
        var now = DateTime.Now;

        var jwtClaims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId),
            new(JwtRegisteredClaimNames.UniqueName, userId)
        };

        if (!email.IsNullOrEmpty())
            jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Email, email!));

        if (!role.IsNullOrEmpty())
            jwtClaims.Add(new Claim(ClaimTypes.Role, role!));

        if (!_audience.IsNullOrEmpty())
            jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Aud, _audience!));

        if (claims?.Any() is true)
        {
            foreach (var (claim, values) in claims)
            {
                jwtClaims.AddRange(values.Select(value => new Claim(claim, value)));
            }
        }

        var expires = now.Add(_expiry);

        var jwt = new JwtSecurityToken(
            _issuer,
            claims: jwtClaims,
            notBefore: now,
            expires: expires,
            signingCredentials: _signingCredentials
        );

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