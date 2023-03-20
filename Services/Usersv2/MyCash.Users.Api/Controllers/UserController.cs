using Micro.Auth.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyCash.Users.Core.Dto;
using MyCash.Users.Core.Entities;
using MyCash.Users.Core.ValueObjects;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyCash.Users.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IJsonWebTokenManager _jsonWebTokenManager;

    public UserController(IConfiguration config, IJsonWebTokenManager jsonWebTokenManager)
    {
        _config = config;
        _jsonWebTokenManager = jsonWebTokenManager;
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult Login([FromBody] UserLogin userLogin)
    {
        var user = Authenticate(userLogin);

        if(user != null)
        {
            //var token = Generate(user);
            var token = _jsonWebTokenManager.CreateToken(user.Id.ToString(), user.Email, user.Role.Name);
            return Ok(token);
        }

        return NotFound("User not found");
    }

    [HttpGet]
    [Authorize(Roles = "ad")]
    public IActionResult AdminsEndpoint()
    {
        var currUser = GetCurrentUser();

        return Ok($"Hi {currUser.Email}");
    }

    private string Generate(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.Name)
        };

        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims, 
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private User Authenticate(UserLogin userLogin)
    {
        var currentUser = new User { Email = userLogin.UserName, Role = new Role { Name = "Admin" } };

        return currentUser;
    }

    private User? GetCurrentUser()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;

        if(identity != null)
        {
            var userClaims = identity.Claims;

            return new User
            {
                Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value
            };
        }

        return null;
    }
}
