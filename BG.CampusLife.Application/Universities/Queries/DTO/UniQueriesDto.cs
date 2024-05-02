using BG.CampusLife.Domain.Entities;
using System.Collections.Generic;

namespace BG.CampusLife.Application.Universities.Queries.DTO
{
    public class UniQueriesDto
    {
        public string Name { get; set; }
        public Domain.Entities.Location Location { get; set; }
        public List<User> Users { get; set; }
    }
}
