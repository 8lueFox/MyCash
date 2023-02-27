using Microsoft.AspNetCore.Mvc;
using MyCash.API;
using MyCash.Wallets.Application.Commands;

namespace MyCash.Wallets.Api.Controllers;

[Route("api/[controller]/[action]")]
public class WalletsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult> CreateUserInvestmentObjects(CreateUserInvestmentObjectsRequest request)
    {
        var response = await Mediator.Send(request);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult> CreateUser(RegisterUserRequest request)
    {
        var response = await Mediator.Send(request);

        return Ok(response);
    }
}
