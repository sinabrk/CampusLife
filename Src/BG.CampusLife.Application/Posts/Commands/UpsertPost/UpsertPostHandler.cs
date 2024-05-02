namespace BG.CampusLife.Application.Posts.Commands.UpsertPost;

public class UpsertPostHandler : BaseHandler<UpsertPostHandler>, IRequestHandler<UpsertPostCommand, PostDto>
{
    public UpsertPostHandler(IMapper mapper, IRepositories repos, ICurrentUserService currentUserService) : base(mapper, repos, currentUserService)
    {}

    public async Task<PostDto> Handle(UpsertPostCommand request, CancellationToken cancellationToken)
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
        
        var result = await Repositories.PostRepository.CreateOrUpdatePost(new Post()
        {
            Id = request.Id,
            Title = request.Title,
            Body = request.Body,
            LocationId = request.LocationId,
            CategoryId = request.CategoryId,
            Status = PostStatus.Approved, // Todo Change in production
            Tags = tags,
            Attachments = attachments,
            UserId = user.Entity.Id,
        }, CurrentUserService.UserId, cancellationToken);

        if (!result.Succeeded) throw new NotFoundException(result.Message);
        
        //> Remove TempDocuments
        await Repositories.DocumentRepository.BulkDeleteTempDocuments(tempDocuments, cancellationToken);

        return Mapper.Map<PostDto>(result.Entity);
    }
}