namespace BG.CampusLife.Application.MarketPlace.Commands.UpsertMarketItem;

public class UpsertMarketItemHandler : BaseHandler<UpsertMarketItemHandler>, IRequestHandler<UpsertMarketItemCommand, MarketItemDto>
{
    public UpsertMarketItemHandler(IMapper mapper, IRepositories repos, ICurrentUserService currentUserService): base(mapper, repos, currentUserService)
    {}

    public async Task<MarketItemDto> Handle(UpsertMarketItemCommand request, CancellationToken cancellationToken)
    {
        var user = await Repositories.UserRepository.GetUserById(CurrentUserService.UserId);
        if (!user.Succeeded) throw new NotFoundException(user.Message);

        //> Attach Images
        var attachments = new List<Document>();
        var tempDocuments = new List<TempDocument>();
        foreach (var id in request.Images)
        {
            var document = await Repositories.DocumentRepository.GetTempDocument(id, CurrentUserService.UserId);
            if (!document.Succeeded) continue;
            attachments.Add(new Document
            {
                Extension = document.Entity.Extension,
                FileName = document.Entity.FileName,
                FilePath = document.Entity.FilePath,
                IsPrimary = document.Entity.IsPrimary,
                ContentType = document.Entity.ContentType,
            });
            tempDocuments.Add(document.Entity);
        }

        //> Tags
        var tags = new List<Tag>();
        var tagsResult = await Repositories.TagRepository.BulkCreateTags(request.Tags, user.Entity, cancellationToken);
        tags.AddRange(tagsResult.Entities);
        
        //> Properties
        var properties = new List<MarketItemProperty>();
        var propertyResult = await Repositories.PropertyRepository.CreateMarketItemProperties(request.Properties);
        properties.AddRange(propertyResult.Entities);
        
        //> Insert or Update Entity
        var result = await Repositories.MarketRepository.CreateOrUpdateMarket(new MarketItem()
        {
            Id = request.Id,
            Title = request.Title,
            Description = request.Description,
            UserId = user.Entity.Id,
            CategoryId = request.CategoryId,
            LocationId = request.LocationId,
            // Status = MarketItemStatuses.ReviewPending, Todo Fix for production
            Status = MarketItemStatuses.Approved,
            Attachments = attachments,
            Properties = properties,
            Tags = tags,
        }, CurrentUserService.UserId, cancellationToken);
        
        //> Remove TempDocuments
        await Repositories.DocumentRepository.BulkDeleteTempDocuments(tempDocuments, cancellationToken);

        return Mapper.Map<MarketItemDto>(result.Entity);
    }
}