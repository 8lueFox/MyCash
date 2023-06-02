using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCash.BackgroundWorker.Application.Services.Interfaces;

namespace MyCash.BackgroundWorker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IBalanceOrganizator _balanceOrganizator;

        public MainController(IBalanceOrganizator balanceOrganizator)
        {
            _balanceOrganizator = balanceOrganizator;
        }

        [HttpGet]
        public void Start()
        {
            _balanceOrganizator.CheckAllFamilies(CancellationToken.None);
        }
    }
}
