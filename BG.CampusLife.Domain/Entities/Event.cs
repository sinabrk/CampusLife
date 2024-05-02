using System;

namespace BG.CampusLife.Domain.Entities
{
    public class Event : Post
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
