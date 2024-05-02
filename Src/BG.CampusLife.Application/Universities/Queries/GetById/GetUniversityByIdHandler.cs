namespace BG.CampusLife.Application.Universities.Queries.GetById;

public class GetUniversityByIdHandler : BaseHandler<GetUniversityByIdHandler>, IRequestHandler<GetUniversityByIdQuery, UniversityDto>
{
    public GetUniversityByIdHandler(IMapper mapper, IRepositories repos) : base(mapper, repos)
    {}

    public async Task<UniversityDto> Handle(GetUniversityByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await Repositories.UniversityRepository.GetUniversityById(request.Id);
        if (!result.Succeeded) throw new NotFoundException(result.Message);
        return Mapper.Map<UniversityDto>(result.Entity);
    }
}