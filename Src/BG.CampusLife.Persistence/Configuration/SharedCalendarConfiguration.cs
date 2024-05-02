namespace BG.CampusLife.Persistence.Configuration;

public class SharedCalendarConfiguration : IEntityTypeConfiguration<SharedCalendar>
{
    public void Configure(EntityTypeBuilder<SharedCalendar> builder)
    {
        builder.HasKey(k => k.Id);
        builder.Property(k => k.Id).ValueGeneratedNever();

        builder.HasQueryFilter(p => !p.IsDeleted);

        builder.HasOne(k => k.Shared)
            .WithMany(k => k.SharedCalendars)
            .HasForeignKey(k => k.SharedUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}