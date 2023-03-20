using MediatR;
using Microsoft.EntityFrameworkCore;
using MyCash.Users.Core.DAL;
using MyCash.Users.Core.Dto;
using MyCash.Users.Core.Services;

namespace MyCash.Users.Core.Queries;

public record GetUserRequest(Guid UserId) : IRequest<UserDto>;

internal class GetUserRequestHandler : IRequestHandler<GetUserRequest, UserDto>
{
    private readonly UserDbContext _userDbContext;
    private readonly ITokenStorage _tokenStorage;

    public GetUserRequestHandler(UserDbContext userDbContext, ITokenStorage tokenStorage)
    {
        _userDbContext = userDbContext;
        _tokenStorage = tokenStorage;
    }

    public async Task<UserDto> Handle(GetUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _userDbContext.Users.SingleOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

        return new UserDto
        {
            UserId = user.Id,
            CreatedAt = user.CreatedAt,
            Email = user.Email,
            Role = user.Role.Name,
            Package = user.Package.Value
        };
    }
}
