using MediatR;
using Microsoft.AspNetCore.Http;

namespace Micro.WebAPI;

public record Request : IRequest
{
    private static HttpContext _httpContext => new HttpContextAccessor().HttpContext;
    public Guid UserId { get; private init; }

    public Request()
    {
        UserId = _httpContext is null ? Guid.Empty : string.IsNullOrWhiteSpace(_httpContext.User.Identity?.Name) ? Guid.Empty : Guid.Parse(_httpContext.User.Identity.Name);
    }
}
public record Request<T> : IRequest<T>
{
    private static HttpContext _httpContext => new HttpContextAccessor().HttpContext;
    public Guid UserId { get; private init; }

    public Request()
    {
        UserId = _httpContext is null ? Guid.Empty : string.IsNullOrWhiteSpace(_httpContext.User.Identity?.Name) ? Guid.Empty : Guid.Parse(_httpContext.User.Identity.Name);
    }
}
