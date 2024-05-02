using System;
using System.Collections.Generic;
using MediatR;

namespace BG.CampusLife.Application.Properties.Queries.GetPropertiesList
{
    public class GetPropertiesListQuery : IRequest<List<PropertyListDto>>
    {
        public string SearchText { get; set; }
        public Guid CategoryId { get; set; }
    }
}