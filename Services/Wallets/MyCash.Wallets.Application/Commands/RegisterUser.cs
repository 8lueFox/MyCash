using MediatR;
using MyCash.Wallets.Core.Entities;
using MyCash.Wallets.Core.Repositories;

namespace MyCash.Wallets.Application.Commands;

public record RegisterUserRequest(string UserPackage) : IRequest;

public class RegisterUserRequestHandler : IRequestHandler<RegisterUserRequest>
{
    private readonly IUserRepository _userRepository;

    public RegisterUserRequestHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var user = new User(Guid.NewGuid(), request.UserPackage);
        await _userRepository.AddAsync(user);
        return Unit.Value;
    }
}
