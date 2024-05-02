namespace BG.CampusLife.Application.Universities.Commands.UpsertUniversity;

public class UpsertUniversityHandler : BaseHandler<UpsertUniversityHandler>, IRequestHandler<UpsertUniversityCommand, PostDto>
{
    public UpsertUniversityHandler(IMapper mapper, IRepositories repos) : base(mapper, repos)
    {}

    public async Task<PostDto> Handle(UpsertUniversityCommand request, CancellationToken cancellationToken)
    {
        var result = await Repositories.UniversityRepository.CreateOrUpdateUniversity(new University()
        {
            Id = request.Id,
            Name = request.Name,
            LocationId = request.LocationId,
            Status = request.Status,
        }, cancellationToken);

        if (!result.Succeeded) throw new NotFoundException(result.Message);

        return Mapper.Map<PostDto>(result.Entity);
    }
}