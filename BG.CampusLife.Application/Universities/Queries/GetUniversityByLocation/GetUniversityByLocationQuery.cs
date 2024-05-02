using MediatR;
using System.Collections.Generic;
using BG.CampusLife.Application.Universities.Queries.DTO;

namespace BG.CampusLife.Application.Universities.Queries.GetUniversityByLocation
{
    public class GetUniversityByLocationQuery : IRequest<List<UniQueriesDto>>
    {
        public Domain.Entities.Location UniverstiyLocation { get; set; }
    }
}
