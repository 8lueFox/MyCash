using Micro.WebAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCash.WealthManager.Application.Commands.CreateFamily;
using MyCash.WealthManager.Application.Queries;

namespace MyCash.WealthManager.Controllers
{
    [Route("api/[controller]/[action]")]
    public class WealthController : BaseController
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public WealthController(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HttpGet]
        public async Task<ActionResult> GetFamilty([FromQuery]GetFamilySummaryRequest request)
            => Ok(await Mediator.Send(request));

        [HttpPost]
        public async Task<ActionResult> CreateFamily(CreateFamilyRequest request)
        {
            var response = await Mediator.Send(request);

            return Ok(response);
        }
    }
}
