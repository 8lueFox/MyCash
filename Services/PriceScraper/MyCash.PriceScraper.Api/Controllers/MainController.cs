using Micro.Framework;
using Micro.WebAPI;
using Microsoft.AspNetCore.Mvc;
using MyCash.PriceScraper.Core.Queries;

namespace MyCash.PriceScraper.Api.Controllers
{
    public class MainController : BaseController
    {
        private readonly AppInfo _appInfo;

        public MainController(AppInfo appInfo)
        {
            _appInfo = appInfo;
        }

        [HttpGet("/")]
        public AppInfo Get()
        {
            return _appInfo;
        }
        [HttpGet("/startScraping")]
        public async Task<ActionResult> StartScraping()
        {
            await Mediator.Send(new FetchStocksRequest());
            return Ok();
        }
    }
}