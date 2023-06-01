using AutoMapper;
using MediatR;
using Micro.WebAPI;
using MyCash.Wallets.Application.DTO;
using MyCash.Wallets.Core.Repositories;

namespace MyCash.Wallets.Application.Queries;

public record GetUserInvestmentObjectsRequest(Guid UserId) : Request<IEnumerable<UserInvestmentObjectsDto>>;

public class GetUserInvestmentObjectsRequestHandler : IRequestHandler<GetUserInvestmentObjectsRequest, IEnumerable<UserInvestmentObjectsDto>>
{
    private readonly IUserInvestmentObjectRepository _userInvestmentObjectRepository;
    private readonly IMapper _mapper;

    public GetUserInvestmentObjectsRequestHandler(IUserInvestmentObjectRepository userInvestmentObjectRepository, IMapper mapper)
    {
        _userInvestmentObjectRepository = userInvestmentObjectRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserInvestmentObjectsDto>> Handle(GetUserInvestmentObjectsRequest request, CancellationToken cancellationToken)
    {
        var uio = await _userInvestmentObjectRepository.GetUserInvestmentObjectsAsync(request.UserId, cancellationToken);

        return _mapper.Map<IEnumerable<UserInvestmentObjectsDto>>(uio);
    }
}