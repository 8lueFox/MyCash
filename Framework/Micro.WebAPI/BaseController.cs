using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Micro.WebAPI;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    private ISender _mediator = null;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    protected static Guid UserId(HttpContext? context)
    => context is null ? Guid.Empty : string.IsNullOrWhiteSpace(context.User.Identity?.Name) ? Guid.Empty : Guid.Parse(context.User.Identity.Name);
}