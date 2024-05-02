namespace BG.CampusLife.Persistence.Configuration;

public class TempDocumentConfiguration : IEntityTypeConfiguration<TempDocument>
{
    public void Configure(EntityTypeBuilder<TempDocument> builder)
    {
        builder.HasKey(k => k.Id);
        builder.Property(k => k.Id).ValueGeneratedNever();
    }
}