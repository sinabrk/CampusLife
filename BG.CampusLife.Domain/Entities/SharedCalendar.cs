using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BG.CampusLife.Domain.Entities
{
    // TODO: Check the relation and resolve it

    /// <summary>
    /// This is for shared calendar entity
    /// first user is authenticated on request
    /// shared user is obvious
    /// in queries to find the shared calendars
    /// we search on shared user id
    /// </summary>
    public class SharedCalendar
    {
        public Guid UserId { get; set; }
        [ForeignKey("UserId")] 
        public User UserNavigation { get; set; }

        public Guid SharedUserId { get; set; }
        [ForeignKey("SharedUserId")]
        public User SharedNavigation { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
    }
}