using MediatR;
using System;
using BG.CampusLife.Application.Universities.Commands.DTO;

namespace BG.CampusLife.Application.Universities.Commands.CreateUniversity
{
    public class CreateUniversityCommand : IRequest<CreateOrUpdateUniversityDto>
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public Domain.Entities.Location Location { get; set; }

        //public Guid LocationId { get; set; }
    }
}
