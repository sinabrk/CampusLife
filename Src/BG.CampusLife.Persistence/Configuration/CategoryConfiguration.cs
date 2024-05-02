namespace BG.CampusLife.Persistence.Configuration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(k => k.Id);
        builder.Property(k => k.Id).ValueGeneratedNever();

        builder.HasQueryFilter(p => !p.IsDeleted);
        
        builder
            .HasOne(x => x.Parent)
            .WithMany(x => x.Children)
            .HasForeignKey(d => d.ParentId)
            .HasConstraintName("FK_Categories_Categories");
    }
}