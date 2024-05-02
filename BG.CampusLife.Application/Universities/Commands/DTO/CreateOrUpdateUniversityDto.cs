using System;

namespace BG.CampusLife.Application.Universities.Commands.DTO
{
    public class CreateOrUpdateUniversityDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Domain.Entities.Location Location { get; set; }
    }
}
