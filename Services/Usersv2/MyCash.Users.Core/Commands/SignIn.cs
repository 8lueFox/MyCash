using MediatR;
using Micro.Auth.JWT;
using Micro.Security.Encryption;
using Microsoft.Extensions.Logging;
using MyCash.Users.Core.Exceptions;
using MyCash.Users.Core.Repositories;
using MyCash.Users.Core.Services;
using System.ComponentModel.DataAnnotations;

namespace MyCash.Users.Core.Commands;

public record SignInRequest(string Email, string Password) : IRequest;

public class SignInRequestHandler : IRequestHandler<SignInRequest>
{
    private static readonly EmailAddressAttribute _emailAddressAttribute = new();
    private readonly IUserRepository _userRepository;
    private readonly IJsonWebTokenManager _jsonWebTokenManager;
    private readonly IPasswordManager _passwordManager;
    private readonly ITokenStorage _tokenStorage;
    private readonly ILogger<SignInRequestHandler> _logger;


    public SignInRequestHandler(
        IUserRepository userRepository, 
        IJsonWebTokenManager jsonWebTokenManager, 
        IPasswordManager passwordManager, 
        ITokenStorage tokenStorage, 
        ILogger<SignInRequestHandler> logger)
    {
        _userRepository = userRepository;
        _jsonWebTokenManager = jsonWebTokenManager;
        _passwordManager = passwordManager;
        _tokenStorage = tokenStorage;
        _logger = logger;
    }

    public async Task Handle(SignInRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || !_emailAddressAttribute.IsValid(request.Email))
        {
            throw new InvalidEmailException(request.Email);
        }

        if (string.IsNullOrWhiteSpace(request.Password))
        {
            throw new MissingPasswordException();
        }

        var user = await _userRepository.GetAsync(request.Email.ToLowerInvariant());
        if (user is null)
            throw new InvalidCredentialsException();

        if (!_passwordManager.IsValid(request.Password, user.Password!))
            throw new InvalidCredentialsException();

        var claims = new Dictionary<string, IEnumerable<string>>
        {
            ["permissions"] = user.Role.Permissions
        };

        var jwt = _jsonWebTokenManager.CreateToken(user.Id.ToString(), user.Email, user.Role.Name, claims);
        jwt.Email = user.Email;

        _logger.LogInformation($"User with ID: {user.Id} has signed in.");

        _tokenStorage.Set(jwt);
    }
}
