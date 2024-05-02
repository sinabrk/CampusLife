namespace BG.CampusLife.Persistence.Configuration;

public class MarketItemConfiguration : IEntityTypeConfiguration<MarketItem>
{
    public void Configure(EntityTypeBuilder<MarketItem> builder)
    {
        builder.HasKey(k => k.Id);
        builder.Property(k => k.Id).ValueGeneratedNever();

        builder.HasQueryFilter(p => !p.IsDeleted);
        
        builder.HasOne(p => p.User).WithMany().OnDelete(DeleteBehavior.NoAction);
    }
}