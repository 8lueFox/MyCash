﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Micro.WebAPI;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    private ISender _mediator = null;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}