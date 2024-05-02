using MediatR;
using System;

namespace BG.CampusLife.Application.Universities.Commands.DeleteUniversity
{
    public class DeleteUniversityByIdCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
