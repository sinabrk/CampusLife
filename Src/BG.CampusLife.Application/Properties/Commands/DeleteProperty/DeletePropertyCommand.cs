using System;
using MediatR;

namespace BG.CampusLife.Application.Properties.Commands.DeleteProperty
{
    public class DeletePropertyCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}