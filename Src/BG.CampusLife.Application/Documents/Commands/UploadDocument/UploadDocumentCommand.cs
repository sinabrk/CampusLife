namespace BG.CampusLife.Application.Documents.Commands.UploadDocument;

public class UploadDocumentCommand : IRequest<UploadDocumentDto>
{
    [Required]
    public IFormFile File { get; set; }

    [Required]
    public bool IsPrimary { get; set; }
}