using System;
using BG.CampusLife.Domain.Enums;
using MediatR;

namespace BG.CampusLife.Application.Properties.Commands.UpsertProperty
{
    public class UpsertPropertyCommand : IRequest<UpsertPropertyDto>
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public PropertyControlTypes ControlType { get; set; }
        public string Name { get; set; }
        public string Options { get; set; }
        public bool Required { get; set; }
    }
}