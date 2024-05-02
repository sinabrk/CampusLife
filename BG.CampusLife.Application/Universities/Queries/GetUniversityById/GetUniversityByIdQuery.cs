using MediatR;
using System;
using BG.CampusLife.Application.Universities.Queries.DTO;

namespace BG.CampusLife.Application.Universities.Queries.GetUniversityById
{
    public class GetUniversityByIdQuery : IRequest<UniQueriesDto>
    {
        public Guid Id { get; set; }
    }
}
