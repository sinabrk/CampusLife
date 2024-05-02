namespace BG.CampusLife.Application.Categories.Commands.DeleteCategory;

public class DeleteCategoryHandler : BaseHandler<DeleteCategoryHandler>, IRequestHandler<DeleteCategoryCommand>
{
    public DeleteCategoryHandler(IMapper mapper, IRepositories repos) : base(mapper, repos)
    {}

    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        Validation(request);
        await Repositories.CategoryRepository.DeleteCategory(request.Id, cancellationToken);

        return Unit.Value;
    }

    private async void Validation(DeleteCategoryCommand request)
    {
        var category = await Repositories.GetEntityById<Category>(request.Id);
        if (category == null)
            throw new NotFoundException(nameof(Category), request.Id);
    }
}