namespace BG.CampusLife.Application.Documents.Commands.UploadDocument;

public class UploadDocumentHandler : BaseHandler<UploadDocumentHandler>, IRequestHandler<UploadDocumentCommand, UploadDocumentDto>
{
    public UploadDocumentHandler(IMapper mapper, IRepositories repos, ICurrentUserService currentUserService) : base(mapper, repos, currentUserService)
    {}

    public async Task<UploadDocumentDto> Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
    {
        if (!IsValidImage(request.File)) throw new BadRequestException("Not valid image");

        var result = await Repositories.DocumentRepository.CreateDocument(request.File, CurrentUserService.UserId,
            GetFileExtension(request.File), request.IsPrimary, cancellationToken);
        return Mapper.Map<UploadDocumentDto>(result.Entity);
    }

    private bool IsValidImage(IFormFile file)
    {
        var validExtensions = new[] { ".jpeg", ".png" };
        return validExtensions.Any(item => item == GetFileExtension(file));
    }

    private string GetFileExtension(IFormFile file) =>
        "." + file.FileName.Split(".")[file.FileName.Split(".").Length - 1];
}