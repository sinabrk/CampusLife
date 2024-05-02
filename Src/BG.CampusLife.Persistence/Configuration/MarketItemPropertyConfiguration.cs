namespace BG.CampusLife.Persistence.Configuration;

public class MarketItemPropertyConfiguration : IEntityTypeConfiguration<MarketItemProperty>
{
    public void Configure(EntityTypeBuilder<MarketItemProperty> builder)
    {
        builder.HasKey(k => k.Id);
        builder.Property(k => k.Id).ValueGeneratedNever();

        builder.HasQueryFilter(p => !p.IsDeleted);

        builder.HasOne(p => p.Property).WithMany().OnDelete(DeleteBehavior.NoAction);
    }
}