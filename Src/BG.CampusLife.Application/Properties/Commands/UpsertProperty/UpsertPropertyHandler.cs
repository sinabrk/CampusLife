namespace BG.CampusLife.Application.Properties.Commands.UpsertProperty;

public class UpsertPropertyHandler : BaseHandler<UpsertPropertyHandler>, IRequestHandler<UpsertPropertyCommand, UpsertPropertyDto>
{
    public UpsertPropertyHandler(IMapper mapper, IRepositories repo) : base(mapper, repo)
    {}

    public async Task<UpsertPropertyDto> Handle(UpsertPropertyCommand request, CancellationToken cancellationToken)
    {
        var result = await Repositories.PropertyRepository.CreateOrUpdateProperty(new Property()
        {
            Id = request.Id,
            CategoryId = request.CategoryId,
            Name = request.Name,
            ControlType = request.ControlType,
            Options = request.Options,
            Required = request.Required,
        }, cancellationToken);

        if (!result.Succeeded) throw new NotFoundException(result.Message);

        return Mapper.Map<UpsertPropertyDto>(result.Entity);
    }
}