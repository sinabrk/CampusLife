using BG.CampusLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using BG.CampusLife.Application.Interfaces.Services;

namespace BG.CampusLife.Infrastructure.Persistence
{
    public class CampusContext : DbContext
    {
        private readonly ICurrentUserService _currentUser;

        public CampusContext(DbContextOptions<CampusContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        
        public CampusContext(DbContextOptions<CampusContext> options, ICurrentUserService currentUserService) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _currentUser = currentUserService;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<SharedCalendar> SharedCalendars { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserProperty> UserProperties { get; set; }
        public DbSet<Friend> Friends { get; set; }
        
        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<Post>().HasDiscriminator<string>("post_type")
                .HasValue<Post>("post_base")
                .HasValue<Event>("event_details");

            model.Entity<SharedCalendar>().HasNoKey();
            model.Entity<UserProperty>().HasNoKey();
        }
    }
}
