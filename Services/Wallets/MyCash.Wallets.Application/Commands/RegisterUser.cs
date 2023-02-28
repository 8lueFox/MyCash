using MediatR;
using MyCash.Wallets.Core.Entities;
using MyCash.Wallets.Core.Repositories;

namespace MyCash.Wallets.Application.Commands;

public record RegisterUserRequest(string UserPackage) : IRequest<Guid>;

public class RegisterUserRequestHandler : IRequestHandler<RegisterUserRequest, Guid>
{
    private readonly IUserRepository _userRepository;

    public RegisterUserRequestHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Guid> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var user = new User(Guid.NewGuid(), request.UserPackage);
        await _userRepository.AddAsync(user);
        return user.Id.Value;
    }
}
