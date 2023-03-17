using Micro.Framework;
using Microsoft.AspNetCore.Mvc;

namespace MyCash.Users.Api.Controllers
{
    [ApiController]
    [Route("")]
    public class MainController : ControllerBase
    {
        private readonly ILogger<MainController> _logger;
        private readonly AppInfo _appInfo;

        public MainController(ILogger<MainController> logger, AppInfo appInfo)
        {
            _logger = logger;
            _appInfo = appInfo;
        }

        [HttpGet]
        public AppInfo Get()
        {
            return _appInfo;
        }
    }
}