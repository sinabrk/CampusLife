using System;
using MediatR;

namespace BG.CampusLife.Application.Properties.Queries.GetPropertyById
{
    public class GetPropertyByIdQuery : IRequest<PropertyDto>
    {
        public Guid Id { get; set; }
    }
}