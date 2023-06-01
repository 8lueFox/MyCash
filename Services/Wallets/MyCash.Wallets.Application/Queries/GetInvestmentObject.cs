using AutoMapper;
using MediatR;
using Micro.WebAPI;
using MyCash.Wallets.Application.DTO;
using MyCash.Wallets.Core.Repositories;

namespace MyCash.Wallets.Application.Queries;

public record GetInvestmentObjectRequest(Guid InvestmentObjectId) : Request<InvestmentObjectDto>;

public class GetInvestmentObjectRequestHandler : IRequestHandler<GetInvestmentObjectRequest, InvestmentObjectDto>
{
    private readonly IUserInvestmentObjectRepository _userInvestmentObjectRepository;
    private readonly IStockRepository _stockRepository;
    private readonly IMapper _mapper;

    public GetInvestmentObjectRequestHandler(IUserInvestmentObjectRepository userInvestmentObjectRepository, IMapper mapper, IStockRepository stockRepository)
    {
        _userInvestmentObjectRepository = userInvestmentObjectRepository;
        _mapper = mapper;
        _stockRepository = stockRepository;
    }

    public async Task<InvestmentObjectDto?> Handle(GetInvestmentObjectRequest request, CancellationToken cancellationToken)
    {
        var io = await _userInvestmentObjectRepository.GetInvestmentObject(request.InvestmentObjectId, cancellationToken);

        if (io is null)
            return null;

        var stock = await _stockRepository.GetStockByName(io.Name, cancellationToken);

        return _mapper.Map<InvestmentObjectDto>((io, stock));
    }
}
