using Microsoft.AspNetCore.Mvc;
using MyCash.API;
using MyCash.Wallets.Application.Commands;

namespace MyCash.Wallets.Api.Controllers;

[Route("api/[controller]/[action]")]
public class WalletsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateUserInvestmentObjects(CreateUserInvestmentObjectsRequest request)
    {
        var response = await Mediator.Send(request);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult> AddTransactionToInvestmentObject(AddTransactionToInvestmentObjectRequest request)
    {
        var response = await Mediator.Send(request);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> AddInvestmentObject(AddInvestmentObjectRequest request)
    {
        var response = await Mediator.Send(request);

        return Ok(response);
    }
}
