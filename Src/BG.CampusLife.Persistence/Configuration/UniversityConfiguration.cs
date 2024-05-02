namespace BG.CampusLife.Persistence.Configuration;

public class UniversityConfiguration : IEntityTypeConfiguration<University>
{
    public void Configure(EntityTypeBuilder<University> builder)
    {
        builder.HasKey(k => k.Id);
        builder.Property(k => k.Id).ValueGeneratedNever();
        
        builder.HasQueryFilter(p => !p.IsDeleted);
    }
}