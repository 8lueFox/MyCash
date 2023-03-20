using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCash.API;
using MyCash.Users.Core.Commands;
using MyCash.Users.Core.Queries;
using MyCash.Users.Core.Services;

namespace MyCash.Users.Api.Controllers;

[Route("api/")]
[ApiController]
public class UsersController : BaseController
{
    private readonly ITokenStorage _tokenStorage;
    private readonly IHttpContextAccessor _context;

    public UsersController(ITokenStorage tokenStorage, IHttpContextAccessor context)
    {
        _tokenStorage = tokenStorage;
        _context = context;
    }

    [HttpPost("sign-up")]
    [AllowAnonymous]
    public async Task<ActionResult> SignUp(SignUpRequest request)
    {
        await Mediator.Send(request);
        return NoContent();
    }

    [HttpPost("sign-in")]
    [AllowAnonymous]
    public async Task<ActionResult> SignIn(SignInRequest request)
    {
        await Mediator.Send(request);
        var jwt = _tokenStorage.Get();
        return Ok(jwt);
    }

    [HttpGet("me")]
    [AllowAnonymous]
    public async Task<ActionResult> GetInfoAboutMe()
    {
        var user = await Mediator.Send(new GetUserRequest(UserId(_context.HttpContext)));
        return user is null ? NotFound() : Ok(user);
    }

    [HttpGet("users")]
    public async Task<ActionResult> GetUser([FromQuery] GetUserRequest request)
    {
        var user = await Mediator.Send(request);
        return user is null ? NotFound() : Ok(user);
    }

    static Guid UserId(HttpContext? context)
        => context is null ? Guid.Empty : string.IsNullOrWhiteSpace(context.User.Identity?.Name) ? Guid.Empty : Guid.Parse(context.User.Identity.Name);

}
