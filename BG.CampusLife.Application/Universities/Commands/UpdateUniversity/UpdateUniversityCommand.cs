using MediatR;
using System;
using BG.CampusLife.Application.Universities.Commands.DTO;

namespace BG.CampusLife.Application.Universities.Commands.UpdateUniversity
{
    public class UpdateUniversityCommand : IRequest<CreateOrUpdateUniversityDto>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Domain.Entities.Location Location { get; set; }
    }
}
