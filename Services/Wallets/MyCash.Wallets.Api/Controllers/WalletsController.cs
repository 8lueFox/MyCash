using Micro.WebAPI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCash.Wallets.Application.Commands;
using MyCash.Wallets.Application.DTO;
using MyCash.Wallets.Application.Queries;

namespace MyCash.Wallets.Api.Controllers;

[Route("api/[controller]/[action]")]
public class WalletsController : BaseController
{
    private readonly IHttpContextAccessor _context;

    public WalletsController(IHttpContextAccessor context)
    {
        _context = context;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Guid>> CreateUserInvestmentObjects(string Name)
    {
        var response = await Mediator.Send(new CreateUserInvestmentObjectsRequest(UserId(_context.HttpContext), Name));

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult> AddTransactionToInvestmentObject(AddTransactionToInvestmentObjectRequest request)
    {
        await Mediator.Send(request);

        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> AddInvestmentObject(AddInvestmentObjectRequest request)
    {
        var response = await Mediator.Send(request);

        return Ok(response);
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<UserInvestmentObjectsDto>>> GetUserInvestmentObjects()
    {
        var response = await Mediator.Send(new GetUserInvestmentObjectsRequest(UserId(HttpContext)));

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<InvestmentObjectDto>> GetInvestmentObject([FromQuery] GetInvestmentObjectRequest request)
    {
        var response = await Mediator.Send(request);

        return Ok(response);
    }

    static Guid UserId(HttpContext? context)
        => context is null ? Guid.Empty : string.IsNullOrWhiteSpace(context.User.Identity?.Name) ? Guid.Empty : Guid.Parse(context.User.Identity.Name);
}
