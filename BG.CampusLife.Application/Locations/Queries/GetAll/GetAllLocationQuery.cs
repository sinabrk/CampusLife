using MediatR;
using System.Collections.Generic;

namespace BG.CampusLife.Application.Locations.Queries.GetAll
{
    public class GetAllLocationQuery : IRequest<List<Domain.Entities.Location>>
    {
    }
}
