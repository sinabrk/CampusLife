
namespace BG.CampusLife.Persistence.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(k => k.Id);
        builder.Property(p => p.Id).ValueGeneratedNever();
        
        builder.HasQueryFilter(p => !p.IsDeleted);
    }
}