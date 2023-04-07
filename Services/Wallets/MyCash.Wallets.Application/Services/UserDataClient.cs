using AutoMapper;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using MyCash.Wallets.Core;
using MyCash.Wallets.Core.Entities;

namespace MyCash.Wallets.Application.Services;

public class UserDataClient : IUserDataClient
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public UserDataClient(IConfiguration configuration, IMapper mapper)
    {
        _configuration = configuration;
        _mapper = mapper;
    }

    public IEnumerable<User> ReturnAllUsers()
    {
        Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcUser"]}");

        var channel = GrpcChannel.ForAddress(_configuration["GrpcUser"]);
        var client = new GrpcUser.GrpcUserClient(channel);
        var request = new GetAllRequest();
        try
        {
            var reply = client.GetAllUsers(request);
            return _mapper.Map<IEnumerable<User>>(reply.User.ToList());
        }catch(Exception ex )
        {
            Console.WriteLine($"--> Couldn't call GRPC Server {ex.Message}");
            return new List<User>();
        }
    }
}