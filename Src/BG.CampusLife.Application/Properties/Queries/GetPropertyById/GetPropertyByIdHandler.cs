namespace BG.CampusLife.Application.Properties.Queries.GetPropertyById;
public class GetPropertyByIdHandler : BaseHandler<GetPropertyByIdHandler>, IRequestHandler<GetPropertyByIdQuery, PropertyDto>
{
    public GetPropertyByIdHandler(IMapper mapper, IRepositories repos) : base(mapper, repos)
    { }

    public async Task<PropertyDto> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await Repositories.PropertyRepository.GetPropertyById(request.Id);
        return Mapper.Map<PropertyDto>(result.Entities);
    }
}