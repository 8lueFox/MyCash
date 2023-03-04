using MediatR;
using MyCash.Wallets.Core.DomainServices;
using MyCash.Wallets.Core.Repositories;

namespace MyCash.Wallets.Application.Commands;

public record AddInvestmentObjectRequest(Guid UserInvestmentObjectsId, string Name, string Type) : IRequest<Guid>;

public class AddInvestmentObjectRequestHandler : IRequestHandler<AddInvestmentObjectRequest, Guid>
{
    private readonly IUserInvestmentObjectsService _userInvestmentObjectsService;
    private readonly IUserInvestmentObjectRepository _userInvestmentObjectsRepository;

    public AddInvestmentObjectRequestHandler(IUserInvestmentObjectsService userInvestmentObjectsService, IUserInvestmentObjectRepository userInvestmentObjectsRepository)
    {
        _userInvestmentObjectsService = userInvestmentObjectsService;
        _userInvestmentObjectsRepository = userInvestmentObjectsRepository;
    }

    public async Task<Guid> Handle(AddInvestmentObjectRequest request, CancellationToken cancellationToken)
    {
        var userInvestmentObject = await _userInvestmentObjectsRepository.GetUserInvestmentObjectAsync(request.UserInvestmentObjectsId, cancellationToken);

        if(userInvestmentObject is null)
            return Guid.Empty;

        var investmentObject = _userInvestmentObjectsService.AddNew(userInvestmentObject, request.Name, request.Type);
        await _userInvestmentObjectsRepository.UpdateAsync(userInvestmentObject, cancellationToken);

        return investmentObject.Id;
    }
}
