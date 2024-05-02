using AutoMapper;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using BG.CampusLife.Application.Universities.Commands.DTO;
using BG.CampusLife.Domain.Enums;
using NLog.Fluent;
using Uni = BG.CampusLife.Domain.Entities.University;

namespace BG.CampusLife.Application.Universities.Commands.UpdateUniversity
{
    public class UpdateUniversityHandler : BaseHandler<UpdateUniversityHandler>,
        IRequestHandler<UpdateUniversityCommand, CreateOrUpdateUniversityDto>
    {
        private IUniversityRepository _repo;

        public UpdateUniversityHandler(IUniversityRepository repo, IMapper mapper,
            ILogger<UpdateUniversityHandler> logger) : base(logger, mapper)
        {
            _repo = repo;
        }

        public async Task<CreateOrUpdateUniversityDto> Handle(UpdateUniversityCommand request,
            CancellationToken cancellationToken)
        {
            var result = await _repo.Update(new Uni()
            {
                Id = request.Id,
                Name = request.Name,
                Location = request.Location ?? null,
            });
            if (result.Succeeded)
                return Mapper.Map<CreateOrUpdateUniversityDto>(result.Entity);
            Logger.LogError(result.Message);
            throw new NotFoundException(result.Message);
        }
    }
}