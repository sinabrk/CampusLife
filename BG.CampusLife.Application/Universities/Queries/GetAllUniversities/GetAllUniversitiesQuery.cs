using MediatR;
using System.Collections.Generic;
using BG.CampusLife.Application.Universities.Queries.DTO;

namespace BG.CampusLife.Application.Universities.Queries.GetAllUniversities
{
    public class GetAllUniversitiesQuery : IRequest<List<UniQueriesDto>>
    {
    }
}
