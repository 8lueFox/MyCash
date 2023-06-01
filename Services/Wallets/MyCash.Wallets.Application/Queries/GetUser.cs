using AutoMapper;
using MediatR;
using MyCash.Wallets.Application.DTO;
using MyCash.Wallets.Core.Repositories;

namespace MyCash.Wallets.Application.Queries;

public record GetUserRequest(Guid Id) : Request<UserDto>;

public class GetUserRequestHandler : IRequestHandler<GetUserRequest, UserDto>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public GetUserRequestHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(GetUserRequest request, CancellationToken cancellationToken)
    {
        var response = await _userRepository.GetAsync(request.Id, cancellationToken);
        return _mapper.Map<UserDto>(await _userRepository.GetAsync(request.Id, cancellationToken));
    }
}
