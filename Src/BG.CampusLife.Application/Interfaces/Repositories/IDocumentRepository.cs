namespace BG.CampusLife.Application.Interfaces.Repositories;

public interface IDocumentRepository
{
    Task<Result<TempDocument>> CreateDocument(IFormFile file, string userId, string extension, bool isPrimary, CancellationToken cancellationToken);
    Task<Result<int>> DeleteDocument(Guid documentId, string userId, CancellationToken cancellationToken);
    Task<Result<int>> DeleteTempDocument(Guid documentId, string userId, CancellationToken cancellationToken);
    Task<Result<int>> BulkDeleteTempDocuments(List<TempDocument> documents, CancellationToken cancellationToken);
    Task<Result<TempDocument>> GetTempDocument(Guid documentId, string userId);
}