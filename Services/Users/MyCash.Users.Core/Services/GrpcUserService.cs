using AutoMapper;
using Grpc.Core;
using MyCash.Users.Core.Repositories;

namespace MyCash.Users.Core.Services;

public class GrpcUserService : GrpcUser.GrpcUserBase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GrpcUserService(IUserRepository userRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async override Task<UserResponse> GetAllUsers(GetAllRequest request, ServerCallContext context)
    {
        var users = await _userRepository.GetAllAsync();
        var usersMapped = _mapper.Map<IEnumerable<GrpcUserModel>>(users);

        var response = new UserResponse();
        response.User.AddRange(usersMapped);

        return response;
    }
}