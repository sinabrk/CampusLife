namespace BG.CampusLife.Persistence;

public class CampusContext : DbContext, ICampusContext
{
    private readonly ICurrentUserService _currentUserService;

    public CampusContext(DbContextOptions<CampusContext> options) : base(options) { }

    public CampusContext(DbContextOptions<CampusContext> options, ICurrentUserService currentUserService) : base(options)
    {
        _currentUserService = currentUserService;
    }

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<UserCalendar> Calendars { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<MarketItem> MarketItems { get; set; }
    public DbSet<MarketItemProperty> MarketItemProperties { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<SharedCalendar> SharedCalendars { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<TempDocument> TempDocuments { get; set; }
    public DbSet<University> Universities { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder model)
    {
        model.ApplyConfigurationsFromAssembly(typeof(CampusContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _currentUserService.UserId;
                    entry.Entity.Created = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _currentUserService.UserId;
                    entry.Entity.LastModified = DateTime.Now;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    public T CreateAndGetEntity<T>(T entity)
    {
        base.Add(entity);
        base.SaveChangesAsync();
        return entity;
    }

    public void DeleteEntity<T>(T entity)
    {
        base.Remove(entity);
        base.SaveChangesAsync();
    }
}