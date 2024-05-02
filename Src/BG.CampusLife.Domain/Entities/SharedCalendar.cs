namespace BG.CampusLife.Domain.Entities;

/// <summary>
/// This is for shared calendar entity
/// first user is authenticated on request
/// shared user is obvious
/// in queries to find the shared calendars
/// we search on shared user id
/// </summary>
public class SharedCalendar : BaseEntity
{
    public Guid UserId { get; set; }
    [ForeignKey("UserId")] 
    public User User { get; set; }

    public Guid SharedUserId { get; set; }
    [ForeignKey("SharedUserId")] 
    public User Shared { get; set; }

}