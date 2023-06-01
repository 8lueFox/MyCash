using AutoMapper;
using MediatR;
using Micro.WebAPI;
using MyCash.WealthManager.Application.DTO;
using MyCash.WealthManager.Core.Repositories;

namespace MyCash.WealthManager.Application.Queries;

public record GetFamilySummaryRequest(Guid FamilyId) : Request<FamilySummaryDto>;

internal class GetFamilySummaryRequestHandler : IRequestHandler<GetFamilySummaryRequest, FamilySummaryDto>
{
    private readonly IFamilyRepository _familyRepository;
    private readonly IMapper _mapper;

    public GetFamilySummaryRequestHandler(IFamilyRepository familyRepository, IMapper mapper)
    {
        _familyRepository = familyRepository;
        _mapper = mapper;
    }

    public async Task<FamilySummaryDto> Handle(GetFamilySummaryRequest request, CancellationToken cancellationToken)
    {
        var family = await _familyRepository.GetFamilyAsync(request.FamilyId, cancellationToken);

        if(family?.UserId.Value == request.UserId)
            return _mapper.Map<FamilySummaryDto>(family);

        return null!;
    }
}
