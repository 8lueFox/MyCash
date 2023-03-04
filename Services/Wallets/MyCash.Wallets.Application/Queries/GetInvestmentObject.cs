using AutoMapper;
using MediatR;
using MyCash.Wallets.Application.DTO;
using MyCash.Wallets.Core.Repositories;

namespace MyCash.Wallets.Application.Queries;

public record GetInvestmentObjectRequest(Guid InvestmentObjectId) : IRequest<InvestmentObjectDto>;

public record GetInvestmentObjectRequestHandler : IRequestHandler<GetInvestmentObjectRequest, InvestmentObjectDto>
{
    private readonly IUserInvestmentObjectRepository _userInvestmentObjectRepository;
    private readonly IMapper _mapper;

    public GetInvestmentObjectRequestHandler(IUserInvestmentObjectRepository userInvestmentObjectRepository, IMapper mapper)
    {
        _userInvestmentObjectRepository = userInvestmentObjectRepository;
        _mapper = mapper;
    }

    public async Task<InvestmentObjectDto> Handle(GetInvestmentObjectRequest request, CancellationToken cancellationToken)
    {
        var response = await _userInvestmentObjectRepository.GetInvestmentObject(request.InvestmentObjectId, cancellationToken);

        return _mapper.Map<InvestmentObjectDto>(response);
    }
}
