using Microsoft.IdentityModel.Tokens;

namespace MyCash.Auth.JWT;

internal sealed record SecurityKeyDetails(SecurityKey Key, string Algorithm);
