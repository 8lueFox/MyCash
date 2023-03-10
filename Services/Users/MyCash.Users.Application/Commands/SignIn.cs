using MediatR;
using Microsoft.Extensions.Logging;
using MyCash.Users.Core.DomainServices;
using MyCash.Users.Core.Repositories;
using System.ComponentModel.DataAnnotations;

namespace MyCash.Users.Application.Commands;

public record SignInRequest(string Email, string Password) : IRequest;

public class SignInRequestHandler : IRequestHandler<SignInRequest>
{
    private static readonly EmailAddressAttribute EmailAddressAttribute = new();
    private readonly IUserRepository _userRepository;
    private readonly ITokenStorage _tokenStorage;
    private readonly ILogger<SignInRequestHandler> _logger;


    public Task<Unit> Handle(SignInRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
