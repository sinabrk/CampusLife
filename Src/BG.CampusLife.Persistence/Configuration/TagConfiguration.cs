namespace BG.CampusLife.Persistence.Configuration;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(k => k.Id);
        builder.Property(p => p.Id).ValueGeneratedNever();

        builder.HasQueryFilter(p => !p.IsDeleted);
    }
}