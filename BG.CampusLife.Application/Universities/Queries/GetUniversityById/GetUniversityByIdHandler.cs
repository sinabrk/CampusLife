using AutoMapper;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using BG.CampusLife.Application.Universities.Queries.DTO;
using BG.CampusLife.Domain.Enums;

namespace BG.CampusLife.Application.Universities.Queries.GetUniversityById
{
    public class GetUniversityByIdHandler : BaseHandler<GetUniversityByIdHandler>,
        IRequestHandler<GetUniversityByIdQuery, UniQueriesDto>
    {
        private readonly IUniversityRepository _repo;

        public GetUniversityByIdHandler(IMapper mapper, IUniversityRepository repo,
            ILogger<GetUniversityByIdHandler> logger) : base(logger, mapper)
        {
            _repo = repo;
        }

        public async Task<UniQueriesDto> Handle(GetUniversityByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _repo.GetUniversityById(request.Id);
            if (result.Succeeded) return Mapper.Map<UniQueriesDto>(result.Entity);
            Logger.LogError(result.Message);
            throw new NotFoundException(result.Message);
        }
    }
}