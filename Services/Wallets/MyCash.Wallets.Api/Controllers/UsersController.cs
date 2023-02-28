using Microsoft.AspNetCore.Mvc;
using MyCash.API;
using MyCash.Wallets.Application.Commands;
using MyCash.Wallets.Application.DTO;
using MyCash.Wallets.Application.Queries;

namespace MyCash.Wallets.Api.Controllers;

public class UsersController : BaseController
{
    [HttpGet]
    public async Task<UserDto> GetUserAsync([FromQuery] GetUserRequest request)
    {
        var response = await Mediator.Send(request);

        return response;
    }

    [HttpPost]
    public async Task<Guid> CreateUserAsync(RegisterUserRequest request)
    {
        var response = await Mediator.Send(request);

        return response;
    }
}
