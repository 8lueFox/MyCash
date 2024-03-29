﻿using MediatR;
using Micro.WebAPI;
using Microsoft.EntityFrameworkCore;
using MyCash.Users.Core.DAL;
using MyCash.Users.Core.Dto;

namespace MyCash.Users.Core.Queries;

public record GetUserRequest() : Request<UserDto>;

internal class GetUserRequestHandler : IRequestHandler<GetUserRequest, UserDto>
{
    private readonly UserDbContext _userDbContext;

    public GetUserRequestHandler(UserDbContext userDbContext)
    {
        _userDbContext = userDbContext;
    }

    public async Task<UserDto> Handle(GetUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _userDbContext
            .Users
            .AsNoTracking()
            .Include(x => x.Role)
            .SingleOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

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
