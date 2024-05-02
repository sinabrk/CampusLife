namespace BG.CampusLife.Application.Interfaces;

public interface ICampusContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}