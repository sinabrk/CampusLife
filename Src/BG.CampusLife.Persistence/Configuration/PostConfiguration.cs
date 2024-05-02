namespace BG.CampusLife.Persistence.Configuration;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(k => k.Id);
        builder.Property(k => k.Id).ValueGeneratedNever();

        builder.HasQueryFilter(p => !p.IsDeleted);

        builder
            .HasDiscriminator<string>("post_type")
            .HasValue<Post>("post_base")
            .HasValue<Event>("event_details");

        builder.HasOne(k => k.User)
            .WithMany(k => k.Posts)
            .HasForeignKey(k => k.UserId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}