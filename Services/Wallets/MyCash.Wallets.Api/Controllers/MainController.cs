using Micro.Framework;
using Micro.WebAPI;
using Microsoft.AspNetCore.Mvc;

namespace MyCash.Wallets.Api.Controllers
{
    [ApiController]
    [Route("")]
    public class MainController : BaseController
    {
        private readonly AppInfo _appInfo;

        public MainController(AppInfo appInfo)
        {
            _appInfo = appInfo;
        }

        [HttpGet]
        public AppInfo Get()
        {
            return _appInfo;
        }
    }
}
