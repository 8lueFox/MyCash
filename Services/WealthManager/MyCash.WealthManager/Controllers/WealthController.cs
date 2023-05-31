using Micro.WebAPI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCash.WealthManager.Application.Commands.AddExpense;
using MyCash.WealthManager.Application.Commands.CreateFamily;
using MyCash.WealthManager.Application.Commands.DeleteExpense;
using MyCash.WealthManager.Application.Commands.DeleteIncome;
using MyCash.WealthManager.Application.Queries;

namespace MyCash.WealthManager.Controllers;

[Route("api/[controller]/[action]")]
[Authorize]
public class WealthController : BaseController
{
    [HttpGet]
    public async Task<ActionResult> GetFamilty([FromQuery] GetFamilySummaryRequest request)
        => Ok(await Mediator.Send(request with { UserId = UserId(HttpContext) }));

    [HttpPost]
    public async Task<ActionResult> CreateFamily(CreateFamilyRequest request)
        => Ok(await Mediator.Send(request with { UserId = UserId(HttpContext) }));

    [HttpPost]
    public async Task<ActionResult> AddExpense(AddExpenseRequest request)
        => Ok(await Mediator.Send(request with { UserId = UserId(HttpContext) }));

    [HttpPost]
    public async Task<ActionResult> AddIncome(AddIncomeRequest request)
        => Ok(await Mediator.Send(request with { UserId = UserId(HttpContext) }));

    [HttpDelete]
    public async Task<ActionResult> DeleteExpense(DeleteExpenseRequest request)
    {
        await Mediator.Send(request with { UserId = UserId(HttpContext) });
        return Ok();
    }
    [HttpDelete]
    public async Task<ActionResult> DeleteIncome(DeleteIncomeRequest request)
    {
        await Mediator.Send(request with { UserId = UserId(HttpContext) });
        return Ok();
    }
}
