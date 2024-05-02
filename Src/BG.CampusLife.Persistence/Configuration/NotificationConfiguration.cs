namespace BG.CampusLife.Persistence.Configuration;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.HasKey(k => k.Id);
        builder.Property(p => p.Id).ValueGeneratedNever();
        
        builder.HasQueryFilter(p => !p.IsDeleted);
    }
}