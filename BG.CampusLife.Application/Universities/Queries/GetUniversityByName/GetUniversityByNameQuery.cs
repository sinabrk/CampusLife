using BG.CampusLife.Application.Universities.Queries.DTO;
using MediatR;

namespace BG.CampusLife.Application.Universities.Queries.GetUniversityByName
{
    public class GetUniversityByNameQuery : IRequest<UniQueriesDto>
    {
        public string Name { get; set; }
    }
}
