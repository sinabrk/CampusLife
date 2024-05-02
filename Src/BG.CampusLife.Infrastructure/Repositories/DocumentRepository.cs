namespace BG.CampusLife.Infrastructure;

public partial class DbRepositories : IDocumentRepository
{
    public async Task<Result<TempDocument>> GetTempDocument(Guid documentId, string userId)
    {
        var result = new Result<TempDocument>
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Successful,
            Entity = await _context.Set<TempDocument>()
                .Where(item => item.Id == documentId && item.CreatedBy == userId)
                .AsNoTracking()
                .FirstOrDefaultAsync()
        };
        
        if (result.Entity is not null) return result;

        result.Message = MessageHelper.ErrorNotFound("TempDocument", documentId.ToString());
        result.StatusCode = ResultStatusCodes.NotFound;
        result.Succeeded = false;

        return result;
    }

    public async Task<Result<TempDocument>> CreateDocument(IFormFile file, string userId, string extension, bool isPrimary, CancellationToken cancellationToken)
    {
        var result = new Result<TempDocument>
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Created,
            Entity = new TempDocument
            {
                ContentType = file.ContentType,
                Created = DateTime.Now,
                IsPrimary = isPrimary,
                Extension = extension,
                FileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss").GetHashCode().ToString("x8"),
            }
        };

        var folderPath = $"/Files/{userId.GetHashCode():x8}/{DateTime.Now:yyyyMM}/";
        var filePath = $"{folderPath}{result.Entity.FileName}{extension}";
        result.Entity.FilePath = filePath;
        
        _context.Set<TempDocument>().Add(result.Entity);
        await _context.SaveChangesAsync(cancellationToken);

        await SaveToStorage(file, folderPath, filePath);

        return result;
    }
    
    public async Task<Result<int>> DeleteTempDocument(Guid documentId, string userId, CancellationToken cancellationToken)
    {
        var result = new Result<int>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.NoContent,
        };

        var entity = await _context.Set<TempDocument>()
            .Where(item => item.Id == documentId && item.CreatedBy == userId)
            .FirstOrDefaultAsync(cancellationToken);

        if (entity is null)
        {
            result.Succeeded = false;
            result.Message = MessageHelper.ErrorNotFound("TempDocument", documentId.ToString());
            result.StatusCode = ResultStatusCodes.NotFound;
            return result;
        }
        
        _context.Set<TempDocument>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return result;
    }

    public async Task<Result<int>> BulkDeleteTempDocuments(List<TempDocument> documents, CancellationToken cancellationToken)
    {
        var result = new Result<int>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.NoContent,
        };
        _context.Set<TempDocument>().RemoveRange(documents);
        await _context.SaveChangesAsync(cancellationToken);

        return result;
    }

    private async Task SaveToStorage(IFormFile file, string folderPath, string filePath)
    {
        folderPath = folderPath.Insert(0, _webHostEnvironment.WebRootPath);
        
        if (Directory.Exists(folderPath) == false)
            Directory.CreateDirectory(folderPath);


        filePath = filePath.Insert(0, _webHostEnvironment.WebRootPath);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);
    }

    private void RemoveFromStorage(string filePath)
    {
        if (File.Exists(_webHostEnvironment.WebRootPath + filePath))
        {
            File.Delete(_webHostEnvironment.WebRootPath + filePath);
        }
    }

    public async Task<Result<int>> DeleteDocument(Guid documentId, string userId, CancellationToken cancellationToken)
    {
        var result = new Result<int>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.NoContent,
        };

        var entity = await _context.Set<Document>()
            .Where(item => item.Id == documentId && item.CreatedBy == userId)
            .FirstOrDefaultAsync(cancellationToken);

        if (entity is null)
        {
            result.Succeeded = false;
            result.Message = MessageHelper.ErrorNotFound("Document", documentId.ToString());
            result.StatusCode = ResultStatusCodes.NotFound;
            return result;
        }

        RemoveFromStorage(entity.FilePath);

        _context.Set<Document>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return result;
    }
}