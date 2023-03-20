using MediatR;
using Micro.Security.Encryption;
using Microsoft.Extensions.Logging;
using MyCash.Users.Core.Entities;
using MyCash.Users.Core.Exceptions;
using MyCash.Users.Core.Repositories;
using MyCash.Users.Core.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace MyCash.Users.Core.Commands;

public record SignUpRequest(string Email, string Password) : IRequest;

internal sealed class SingUpRequestHandler : IRequestHandler<SignUpRequest>
{
    private static readonly EmailAddressAttribute emailAddressAtribute = new();
    private static readonly string DefaultRole = Role.Default;

    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly ILogger<SingUpRequestHandler> _logger;

    public SingUpRequestHandler
        (IUserRepository userRepository, 
        IRoleRepository roleRepository, 
        IPasswordManager passwordManager, 
        ILogger<SingUpRequestHandler> logger)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _passwordManager = passwordManager;
        _logger = logger;
    }

    public async Task Handle(SignUpRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || !emailAddressAtribute.IsValid(request.Email))
            throw new InvalidEmailException(request.Email);

        if (string.IsNullOrWhiteSpace(request.Password))
            throw new MissingPasswordException();

        var email = request.Email.ToLowerInvariant();
        var user = await _userRepository.GetAsync(email);

        if (user is not null)
            throw new EmailInUseException(email);

        var role = await _roleRepository.GetAsync(DefaultRole);

        ///TODO: Change getting current date from the service IClock
        var now = DateTime.Now;

        var pswd = _passwordManager.Secure(request.Password);

        user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            Password = pswd,
            Role = role,
            CreatedAt = now,
            Package = UserPackage.Standard
        };

        await _userRepository.AddAsync(user);
        _logger.LogInformation($"User with ID: '{user.Id}' has signed up.");
    }
}
