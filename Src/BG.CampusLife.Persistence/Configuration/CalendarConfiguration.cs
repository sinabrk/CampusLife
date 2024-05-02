namespace BG.CampusLife.Persistence.Configuration;

public class CalendarConfiguration : IEntityTypeConfiguration<UserCalendar>
{
    public void Configure(EntityTypeBuilder<UserCalendar> builder)
    {
        builder.HasKey(k => k.Id);
        builder.Property(k => k.Id).ValueGeneratedNever();

        builder.HasQueryFilter(p => !p.IsDeleted);
    }
}